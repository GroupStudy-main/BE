using API.SignalRHub.Tracker;
using APIExtension.ClaimsPrinciple;
using DataLayer.DBObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using RepositoryLayer.Interface;
using ShareResource.DTO;
using ShareResource.DTO.Connection;

namespace API.SignalRHub
{
    [Authorize]
    public class MeetHub : Hub
    {
        //IMapper _mapper;
        IHubContext<PresenceHub> presenceHubContext;
        PresenceTracker presenceTracker;
        IRepoWrapper repos;
        ShareScreenTracker shareScreenTracker;

        public MeetHub(IRepoWrapper repos, ShareScreenTracker shareScreenTracker, PresenceTracker presenceTracker, IHubContext<PresenceHub> presenceHubContext)
        {
            //Console.WriteLine("2.   " + new String('+', 50));
            //Console.WriteLine("2.   Hub/Chat: ctor(IUnitOfWork, UserShareScreenTracker, PresenceTracker, PresenceHub)");

            //_mapper = mapper;
            this.repos = repos;
            this.presenceTracker = presenceTracker;
            this.presenceHubContext = presenceHubContext;
            this.shareScreenTracker = shareScreenTracker;
        }
         /// <summary>
         /// Connect gửi meetingIdString
         /// Group theo meetingId  , Conne
         /// </summary>
         /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            //Console.WriteLine("2.   " + new String('+', 50));
            //Console.WriteLine("2.   Hub/Chat: OnConnectedAsync()");
            FunctionTracker.Instance().AddHubFunc("Hub/Chat: OnConnectedAsync()");
            var httpContext = Context.GetHttpContext();
            var meetingIdString = httpContext.Request.Query["meetingIdString"].ToString();

            var meetingIdInt = int.Parse(meetingIdString);
            Meeting meeting = await repos.Meetings.GetMeetingById(meetingIdInt);
            var roomIdInt = meeting.Id;
            var contextUsername = Context.User.GetUsername();

            await presenceTracker.UserConnected(new UserConnectionDto(contextUsername, roomIdInt), Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, roomIdInt.ToString());//khi user click vao room se join vao
            await AddConnectionToMeeting(meetingIdInt); // luu db DbSet<Connection> de khi disconnect biet

            //Gửi lên Hub thông báo user đã online 
            var currentUser = await repos.Accounts.GetByUsernameAsync(contextUsername);
            await Clients.Group(meetingIdString).SendAsync("UserOnlineInGroup", currentUser);

            var usersInRoom = await presenceTracker.GetOnlineUsers(roomIdInt);
            await repos.Meetings.UpdateCountMember(roomIdInt, usersInRoom.Length);

            var currentConnections = await presenceTracker.GetConnectionsForUser(new UserConnectionDto(contextUsername, roomIdInt));
            await presenceHubContext.Clients.AllExcept(currentConnections).SendAsync("CountMemberInGroup",
                   new { roomId = roomIdInt, countMember = usersInRoom.Length });

            //share screen user vao sau cung
            var userIsSharing = await shareScreenTracker.GetUserIsSharing(roomIdInt);
            if (userIsSharing != null)
            {
                List<string> currentBeginConnectionsUser = await presenceTracker.GetConnectionsForUser(userIsSharing);
                if (currentBeginConnectionsUser.Count > 0)
                    await Clients.Clients(currentBeginConnectionsUser).SendAsync("OnShareScreenLastUser", new { usernameTo = contextUsername, isShare = true });
                await Clients.Caller.SendAsync("OnUserIsSharing", userIsSharing.UserName);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //Console.WriteLine("2.   " + new String('+', 50));
            //Console.WriteLine("2.   Hub/Chat: OnDisconnectedAsync(Exception)");
            FunctionTracker.Instance().AddHubFunc("Hub/Chat: OnDisconnectedAsync(Exception)");
            var username = Context.User.GetUsername();
            var meeting = await RemoveConnectionFromGroup();
            var isOffline = await presenceTracker.UserDisconnected(new UserConnectionDto(username, meeting.Id), Context.ConnectionId);

            await shareScreenTracker.DisconnectedByUser(username, meeting.Id);
            if (isOffline)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, meeting.Id.ToString());
                var temp = await repos.Accounts.GetByUsernameAsync(username);
                await Clients.Group(meeting.Id.ToString()).SendAsync("UserOfflineInGroup", temp);

                var currentUsers = await presenceTracker.GetOnlineUsers(meeting.Id);

                await repos.Meetings.UpdateCountMember(meeting.Id, currentUsers.Length);

                await presenceHubContext.Clients.All.SendAsync("CountMemberInGroup",
                       new { roomId = meeting.Id, countMember = currentUsers.Length });
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(CreateMessageDto createMessageDto)
        {
            //Console.WriteLine("2.   " + new String('+', 50));
            //Console.WriteLine("2.   Hub/Chat: SendMessage(CreateMessageDto)");
            FunctionTracker.Instance().AddHubFunc("Hub/Chat: SendMessage(CreateMessageDto)");
            var userName = Context.User.GetUsername();
            var sender = await repos.Accounts.GetByUsernameAsync(userName);

            var meetingGroup = await repos.Meetings.GetMeetingForConnection(Context.ConnectionId);

            if (meetingGroup != null)
            {
                var message = new MessageDto
                {
                    SenderUsername = userName,
                    SenderDisplayName = sender.Username,
                    Content = createMessageDto.Content,
                    MessageSent = DateTime.Now
                };
                //Luu message vao db
                //code here
                //send meaasge to meetingGroup
                await Clients.Group(meetingGroup.Id.ToString()).SendAsync("NewMessage", message);
            }
        }

        public async Task MuteMicro(bool muteMicro)
        {
            //Console.WriteLine("2.   " + new String('+', 50));
            //Console.WriteLine("2.   Hub/Chat: MuteMicro(bool)");
            FunctionTracker.Instance().AddHubFunc("Hub/Chat: MuteMicro(bool)");
            var meetingGroup = await repos.Meetings.GetMeetingForConnection(Context.ConnectionId);
            if (meetingGroup != null)
            {
                await Clients.Group(meetingGroup.Id.ToString()).SendAsync("OnMuteMicro", new { username = Context.User.GetUsername(), mute = muteMicro });
            }
            else
            {
                throw new HubException("meetingGroup == null");
            }
        }

        public async Task MuteCamera(bool muteCamera)
        {
            //Console.WriteLine("2.   " + new String('+', 50));
            //Console.WriteLine("2.   Hub/Chat: MuteCamera(bool)");
            FunctionTracker.Instance().AddHubFunc("Hub/Chat: MuteCamera(bool)");
            var meetingGroup = await repos.Meetings.GetMeetingForConnection(Context.ConnectionId);
            if (meetingGroup != null)
            {
                await Clients.Group(meetingGroup.Id.ToString()).SendAsync("OnMuteCamera", new { username = Context.User.GetUsername(), mute = muteCamera });
            }
            else
            {
                throw new HubException("meetingGroup == null");
            }
        }

        public async Task ShareScreen(int roomid, bool isShareScreen)
        {
            //Console.WriteLine("2.   " + new String('+', 50));
            //Console.WriteLine("2.   Hub/Chat: ShareScreen(id, bool)");
            FunctionTracker.Instance().AddHubFunc("Hub/Chat: ShareScreen(id, bool)");
            if (isShareScreen)//true is doing share
            {
                await shareScreenTracker.UserConnectedToShareScreen(new UserConnectionDto(Context.User.GetUsername(), roomid));
                await Clients.Group(roomid.ToString()).SendAsync("OnUserIsSharing", Context.User.GetUsername());
            }
            else
            {
                await shareScreenTracker.UserDisconnectedShareScreen(new UserConnectionDto(Context.User.GetUsername(), roomid));
            }
            await Clients.Group(roomid.ToString()).SendAsync("OnShareScreen", isShareScreen);
            //var meetingGroup = await _unitOfWork.Rooms.GetMeetingForConnection(Context.ConnectionId);
        }

        public async Task ShareScreenToUser(int roomid, string username, bool isShare)
        {
            //Console.WriteLine("2.   " + new String('+', 50));
            //Console.WriteLine("2.   Hub/Chat: ShareScreenToUser(id, contextUsername, bool)");
            FunctionTracker.Instance().AddHubFunc("Hub/Chat: ShareScreenToUser(id, contextUsername, bool)");
            var currentBeginConnectionsUser = await presenceTracker.GetConnectionsForUser(new UserConnectionDto(username, roomid));
            if (currentBeginConnectionsUser.Count > 0)
                await Clients.Clients(currentBeginConnectionsUser).SendAsync("OnShareScreen", isShare);
        }

        private async Task<Meeting> RemoveConnectionFromGroup()
        {
            //Console.WriteLine("2.   " + new String('+', 50));
            //Console.WriteLine("2.   Hub/Chat: RemoveConnectionFromGroup()");
            FunctionTracker.Instance().AddHubFunc("Hub/Chat: RemoveConnectionFromGroup()");
            Meeting group = await repos.Meetings.GetMeetingForConnection(Context.ConnectionId);
            var connection = group.Connections.FirstOrDefault(x => x.Id == Context.ConnectionId);
            repos.Meetings.RemoveConnection(connection);

           return group;
        }

        private async Task<Meeting> AddConnectionToMeeting(int meetingId)
        {
            //Console.WriteLine("2.   " + new String('+', 50));
            //Console.WriteLine("2.   Hub/Chat: AddConnectionToMeeting(meetingIdString)");
            FunctionTracker.Instance().AddHubFunc("Hub/Chat: AddConnectionToMeeting(meetingIdString)");
            var meeting = await repos.Meetings.GetByIdAsync(meetingId);
            var connection = new Connection { 
                Id=Context.ConnectionId, 
                AccountId= Context.User.GetUserId() 
            };
            if (meeting != null)
            {
                meeting.Connections.Add(connection);
            }

            return meeting;
        }
    }
}
