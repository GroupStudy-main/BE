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
    public class MeetingHub : Hub
    {
        //Thông báo có người mới vào meeting
        //BE SendAsync(UserOnlineInGroupMsg, MemberSignalrDto)
        public static string UserOnlineInMeetingMsg => "UserOnlineInMeeting";

        //Dùng để test coi có connect thành công chưa. Nếu connect thành công, be sẽ
        //gửi 1 đoạn msg thông báo là đã connect meetingHub thành công
        //BE SendAsync(OnConnectMeetHubSuccessfullyMsg, msg: string);
        public static string OnConnectMeetHubSuccessfullyMsg => "OnConnectMeetHubSuccessfully";
        //Dùng để test coi có invoke thành công không. Nếu invoke thành công, be sẻ 
        //gửi 1 đoạn msg thông báo đã invoke thành công
        public static string OnTestReceiveInvokeMsg => "OnTestReceiveInvoke";

        //Thông báo có người rời meeting
        //BE SendAsync(UserOfflineInGroupMsg, offlineUser: MemberSignalrDto)
        public static string UserOfflineInMeetingMsg => "UserOfflineInMeeting";
       
        //Thông báo có user nào đang show screen ko. Cho FE biết để chuyển  
        //màn hình chính qua lại chế độ show các cam và chế độ share screen
        //BE SendAsync(OnShareScreenMsg, isShareScreen: bool)
        public static string OnShareScreenMsg => "OnShareScreen";

        //Thông báo tới người đang share screen là có người mới, shareScreenPeer share luôn cho người này
        //BE SendAsync(OnShareScreenLastUser, new { usernameTo: string, isShare: bool })
        public static string OnShareScreenLastUser => "OnShareScreenLastUser";

        //Thông báo người nào đang share screen
        //SendAsync(OnUserIsSharingMsg, screenSharerUsername: string);
        public static string OnUserIsSharingMsg => "OnUserIsSharing";

        //Thông báo tình trạng muteCam của username. Chỉ dùng để thay đổi icon cam trên 
        //màn hình của người khác. Việc truyền cam là do peer trên FE quyết định
        //BE SendAsync(OnMuteCameraMsg, new { username: String, mute: bool })
        public static string OnMuteCameraMsg => "OnMuteCamera";

        //Thông báo tình trạng muteMic của username. Chỉ dùng để thay đổi icon mic trên 
        //màn hình của người khác. Việc truyền mic hay không là do peer trên FE quyết định
        //SendAsync(OnMuteMicroMsg, new { username: String, mute: bool })
        public static string OnMuteMicroMsg => "OnMuteMicro";

        //Thông báo có Chat Message mới
        //BE SendAsync("NewMessage", MessageSignalrGetDto)
        public static string NewMessageMsg => "NewMessage";



        //IMapper _mapper;
        IHubContext<GroupHub> groupHub;
        PresenceTracker presenceTracker;
        IRepoWrapper repos;
        ShareScreenTracker shareScreenTracker;

        public MeetingHub(IRepoWrapper repos, ShareScreenTracker shareScreenTracker, PresenceTracker presenceTracker, IHubContext<GroupHub> presenceHubContext)
        {
            //Console.WriteLine("2.   " + new String('+', 50));
            //Console.WriteLine("2.   Hub/Chat: ctor(IUnitOfWork, UserShareScreenTracker, PresenceTracker, PresenceHub)");

            //_mapper = mapper;
            this.repos = repos;
            this.presenceTracker = presenceTracker;
            this.groupHub = presenceHubContext;
            this.shareScreenTracker = shareScreenTracker;
        }
        #region old code
        /// <summary>
        /// Connect gửi meetingIdString
        /// Group theo meetingId  , Conne
        /// </summary>
        /// <returns></returns>
        //public override async Task OnConnectedAsync()
        //{
        //    //Console.WriteLine("2.   " + new String('+', 50));
        //    //Console.WriteLine("2.   Hub/Chat: OnConnectedAsync()");
        //    FunctionTracker.Instance().AddHubFunc("Hub/Chat: OnConnectedAsync()");
        //    var httpContext = Context.GetHttpContext();
        //    string meetingIdString = httpContext.Request.Query["meetingIdString"].ToString();

        //    int meetingIdInt = int.Parse(meetingIdString);
        //    Meeting meeting = await repos.Meetings.GetMeetingById(meetingIdInt);
        //    int roomIdInt = meeting.Id;
        //    string contextUsername = Context.User.GetUsername();

        //    await presenceTracker.UserConnected(new UserConnectionSignalrDto(contextUsername, roomIdInt), Context.ConnectionId);

        //    await Groups.AddToGroupAsync(Context.ConnectionId, roomIdInt.ToString());//khi user click vao room se join vao
        //    await AddConnectionToMeeting(meetingIdInt); // luu db DbSet<Connection> de khi disconnect biet

        //    //Gửi lên Hub thông báo user đã online 
        //    Account currentUser = await repos.Accounts.GetByUsernameAsync(contextUsername);
        //    await Clients.Group(meetingIdString).SendAsync("UserOnlineInGroup", currentUser);

        //    UserConnectionSignalrDto[] usersInRoom = await presenceTracker.GetOnlineUsersInMeet(roomIdInt);
        //    await repos.Meetings.UpdateCountMember(roomIdInt, usersInRoom.Length);

        //    List<string> currentConnections = await presenceTracker.GetConnectionIdsForUser(new UserConnectionSignalrDto(contextUsername, roomIdInt));
        //    await presenceHubContext.Clients.AllExcept(currentConnections).SendAsync("CountMemberInGroup",
        //           new { roomId = roomIdInt, countMember = usersInRoom.Length });

        //    //share screen user vao sau cung
        //    UserConnectionSignalrDto userIsSharing = await shareScreenTracker.GetUserIsSharingScreenForMeeting(roomIdInt);
        //    if (userIsSharing != null)
        //    {
        //        List<string> currentBeginConnectionsUser = await presenceTracker.GetConnectionIdsForUser(userIsSharing);
        //        if (currentBeginConnectionsUser.Count > 0)
        //            await Clients.Clients(currentBeginConnectionsUser).SendAsync("OnShareScreenLastUser", new { usernameTo = contextUsername, isShare = true });
        //        await Clients.Caller.SendAsync("OnUserIsSharing", userIsSharing.UserName);
        //    }
        //}
        #endregion
        //sẽ dc gọi khi FE sẽ connect qua hàm này
        //sẽ dc gọi khi FE gọi:
        //this.chatHubConnection = new HubConnectionBuilder()
        //    .withUrl(this.hubUrl + 'chathub?roomId=' + roomId, {
        //        accessTokenFactory: () => user.token
        //    }).withAutomaticReconnect().build()
        //this.chatHubConnection.start().catch(err => console.log(err));
        public override async Task OnConnectedAsync()
        {
            FunctionTracker.Instance().AddHubFunc("Hub/Chat: OnConnectedAsync()");
            //Step 1: Lấy meeting Id và username
            HttpContext httpContext = Context.GetHttpContext();
            string meetingIdString = httpContext.Request.Query["meetingId"].ToString();
            int meetingIdInt = int.Parse(meetingIdString);
            string username = Context.User.GetUsername();
            //Step 2: Add ContextConnection vào MeetingHub.Group(meetingId) và add (user, meeting) vào presenceTracker
            await presenceTracker.UserConnected(new UserConnectionSignalrDto(username, meetingIdInt), Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, meetingIdString);//khi user click vao room se join vao
                                                                                //await AddConnectionToGroup(meetingIdInt); // luu db DbSet<Connection> de khi disconnect biet

            //Step 3: Tạo Connect để lưu vào DB, ConnectionId
            #region lưu Db Connection
            Meeting meeting = await repos.Meetings.GetMeetingByIdSignalr(meetingIdInt);
            //Connection connection = new Connection(Context.ConnectionId, Context.User.GetUsername());
            Connection connection = new Connection {
                Id = Context.ConnectionId,
                AccountId = Context.User.GetUserId(),
                MeetingId = meetingIdInt,
                UserName = Context.User.GetUsername(),
                Start = DateTime.Now
            };
            if (meeting != null)
            {
                //meeting.Connections.Add(connection);
                repos.Connections.CreateConnectionSignalrAsync(connection);
            }
          
            #endregion

            //var usersOnline = await _unitOfWork.UserRepository.GetUsersOnlineAsync(currentUsers);
            //Step 4: Thông báo với meetHub.Group(meetingId) là mày đã online  SendAsync(UserOnlineInGroupMsg, MemberSignalrDto)
            MemberSignalrDto currentUserDto = await repos.Accounts.GetMemberSignalrAsync(username);
            await Clients.Group(meetingIdString).SendAsync(UserOnlineInMeetingMsg, currentUserDto);
            Console.WriteLine("2.1     " + new String('+', 50));
            Console.WriteLine("2.1     Hub/ChatSend: UserOnlineInGroupMsg, MemberSignalrDto");
            FunctionTracker.Instance().AddHubFunc("Hub/ChatSend: UserOnlineInGroupMsg, MemberSignalrDto");

            //Step 5: Update số người trong meeting lên db
            UserConnectionSignalrDto[] currentUsersInMeeting = await presenceTracker.GetOnlineUsersInMeet(meetingIdInt);
            await repos.Meetings.UpdateCountMemberSignalr(meetingIdInt, currentUsersInMeeting.Length);

            //Test
            await Clients.Caller.SendAsync(OnConnectMeetHubSuccessfullyMsg, $"Connect meethub dc r! {username} vô dc r ae ơi!!!");

            // Step 6: Thông báo với groupHub.Group(groupId) số người ở trong phòng  
            List<string> currentConnectionIds = await presenceTracker.GetConnectionIdsForUser(new UserConnectionSignalrDto(username, meetingIdInt));
            Console.WriteLine("2.1     " + new String('+', 50));
            Console.WriteLine("2.1     Hub/PresenceSend: CountMemberInGroupMsg, { meetingId, countMember }");
            FunctionTracker.Instance().AddHubFunc("Hub/PresenceSend: CountMemberInGroupMsg, { meetingId, countMember }");
            await groupHub.Clients.AllExcept(currentConnectionIds).SendAsync(GroupHub.CountMemberInGroupMsg,
                   new { meetingId = meetingIdInt, countMember = currentUsersInMeeting.Length });

            //share screen cho user vao sau cung
            //step 7: Thông báo shareScreen cho user vào cuối 
            UserConnectionSignalrDto userIsSharing = await shareScreenTracker.GetUserIsSharingScreenForMeeting(meetingIdInt);
            if (userIsSharing != null)
            {
                List<string> sharingUserConnectionIds = await presenceTracker.GetConnectionIdsForUser(userIsSharing);
                if (sharingUserConnectionIds.Count > 0)
                {
                    await Clients.Clients(sharingUserConnectionIds).SendAsync(OnShareScreenLastUser, new { usernameTo = username, isShare = true });
                }

                await Clients.Caller.SendAsync(OnUserIsSharingMsg, userIsSharing.Username);
            }
        }

        #region old code
        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    //Console.WriteLine("2.   " + new String('+', 50));
        //    //Console.WriteLine("2.   Hub/Chat: OnDisconnectedAsync(Exception)");
        //    FunctionTracker.Instance().AddHubFunc("Hub/Chat: OnDisconnectedAsync(Exception)");
        //    string username = Context.User.GetUsername();
        //    Meeting meeting = await RemoveConnectionFromGroup();
        //    bool isOffline = await presenceTracker.UserDisconnected(new UserConnectionSignalrDto(username, meeting.Id), Context.ConnectionId);

        //    await shareScreenTracker.RemoveUserShareScreen(username, meeting.Id);
        //    if (isOffline)
        //    {
        //        await Groups.RemoveFromGroupAsync(Context.ConnectionId, meeting.Id.ToString());
        //        Account temp = await repos.Accounts.GetByUsernameAsync(username);
        //        await Clients.Group(meeting.Id.ToString()).SendAsync("UserOfflineInGroup", temp);

        //        UserConnectionSignalrDto[] currentUsers = await presenceTracker.GetOnlineUsersInMeet(meeting.Id);

        //        await repos.Meetings.UpdateCountMember(meeting.Id, currentUsers.Length);

        //        await presenceHubContext.Clients.All.SendAsync("CountMemberInGroup",
        //               new { roomId = meeting.Id, countMember = currentUsers.Length });
        //    }
        //    await base.OnDisconnectedAsync(exception);
        //}
        #endregion
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            FunctionTracker.Instance().AddHubFunc("Hub/Chat: OnDisconnectedAsync(Exception)");
            //step 1: Lấy username 
            string username = Context.User.GetUsername();
            //step 2: Xóa connection trong db và lấy meeting
            Meeting meeting = await RemoveConnectionFromMeeting();

            //step 3: Xóa ContextConnectionId khỏi presenceTracker và check xem user còn connect nào khác với meeting ko
            bool isOffline = await presenceTracker.UserDisconnected(new UserConnectionSignalrDto(username, meeting.Id), Context.ConnectionId);

            //step 4: Remove khỏi shareScreenTracker nếu có
            await shareScreenTracker.RemoveUserShareScreen(username, meeting.Id);

            //step 5: Remove ContextConnectionId khỏi meetingHub.Group(meetingId)   chắc move ra khỏi if
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, meeting.Id.ToString());
            if (isOffline)
            {
                ////step 5: Nếu ko còn connect nào nữa thì remove ContextConnectionId khỏi meetingHub.Group(meetingId)   chắc move ra khỏi if
                //await Groups.RemoveFromGroupAsync(Context.ConnectionId, meeting.RoomId.ToString());

                //step 6: Nếu ko còn connect nào nữa thì Thông báo với meetingHub.Group(meetingId)
                MemberSignalrDto offLineUser = await repos.Accounts.GetMemberSignalrAsync(username);
                await Clients.Group(meeting.Id.ToString()).SendAsync(UserOfflineInMeetingMsg, offLineUser);

                //step 7: Update lại số người trong phòng
                UserConnectionSignalrDto[] currentUsersInMeeting = await presenceTracker.GetOnlineUsersInMeet(meeting.Id);
                await repos.Meetings.UpdateCountMemberSignalr(meeting.Id, currentUsersInMeeting.Length);

                //await presenceHub.Clients.All.SendAsync("CountMemberInGroup",
                //       new { roomId = group.RoomId, countMember = currentUsers.Length });

                //step 8: Thông báo với groupHub.Group(groupId) số người ở trong phòng
                await groupHub.Clients.All.SendAsync(GroupHub.CountMemberInGroupMsg,
                       new { meetingId = meeting.Id, countMember = currentUsersInMeeting.Length });
            }
            //step 9: Disconnect khỏi meetHub
            await base.OnDisconnectedAsync(exception);
        }

        #region old code
        //public async Task ShareScreen(int roomid, bool isShareScreen)
        //{
        //    //Console.WriteLine("2.   " + new String('+', 50));
        //    //Console.WriteLine("2.   Hub/Chat: ShareScreen(id, bool)");
        //    FunctionTracker.Instance().AddHubFunc("Hub/Chat: ShareScreen(id, bool)");
        //    if (isShareScreen)//true is doing share
        //    {
        //        await shareScreenTracker.UserConnectedToShareScreen(new UserConnectionSignalrDto(Context.User.GetUsername(), roomid));
        //        await Clients.Group(roomid.ToString()).SendAsync("OnUserIsSharing", Context.User.GetUsername());
        //    }
        //    else
        //    {
        //        await shareScreenTracker.RemoveUserShareScreen(new UserConnectionSignalrDto(Context.User.GetUsername(), roomid));
        //    }
        //    await Clients.Group(roomid.ToString()).SendAsync("OnShareScreen", isShareScreen);
        //    //var meetingGroup = await _unitOfWork.Rooms.GetMeetingForConnection(Context.ConnectionId);
        //}
        #endregion
        //Có trong flow người mới vào meeting và flow có người bật share screen
        //sẽ dc gọi khi FE gọi chatHubConnection.invoke('ShareScreen', roomId, isShareScreen)
        public async Task ShareScreen(int meetingId, bool isShareScreen)
        {
            FunctionTracker.Instance().AddHubFunc("Hub/Chat: ShareScreen(id, bool)");
            if (isShareScreen)//true is doing share
            {
                await shareScreenTracker.AddUserSharingScreen(new UserConnectionSignalrDto(Context.User.GetUsername(), meetingId));
                await Clients.Group(meetingId.ToString()).SendAsync(OnUserIsSharingMsg, Context.User.GetUsername());
            }
            else
            {
                await shareScreenTracker.RemoveUserShareScreen(new UserConnectionSignalrDto(Context.User.GetUsername(), meetingId));
            }
            await Clients.Group(meetingId.ToString()).SendAsync(OnShareScreenMsg, isShareScreen);
            //var group = await _unitOfWork.RoomRepository.GetRoomForConnection(Context.ConnectionId);
        }

        #region old code ShareScreenToUser
        //public async Task ShareScreenToUser(int roomid, string username, bool isShare)
        //{
        //    //Console.WriteLine("2.   " + new String('+', 50));
        //    //Console.WriteLine("2.   Hub/Chat: ShareScreenToUser(id, contextUsername, bool)");
        //    FunctionTracker.Instance().AddHubFunc("Hub/Chat: ShareScreenToUser(id, contextUsername, bool)");
        //    List<string> currentBeginConnectionsUser = await presenceTracker.GetConnectionIdsForUser(new UserConnectionSignalrDto(username, roomid));
        //    if (currentBeginConnectionsUser.Count > 0)
        //        await Clients.Clients(currentBeginConnectionsUser).SendAsync("OnShareScreen", isShare);
        //}
        #endregion
        public async Task ShareScreenToUser(int meetingId, string receiverUsername, bool isShare)
        {
            FunctionTracker.Instance().AddHubFunc("Hub/Chat: ShareScreenToUser(id, username, bool)");
            var ReceiverConnectionIds = await presenceTracker.GetConnectionIdsForUser(new UserConnectionSignalrDto(receiverUsername, meetingId));
            if (ReceiverConnectionIds.Count > 0)
                await Clients.Clients(ReceiverConnectionIds).SendAsync(OnShareScreenMsg, isShare);
        }

        #region old map
        //public async Task SendMessage(MessageSignalRCreateDto createMessageDto)
        //{
        //    //Console.WriteLine("2.   " + new String('+', 50));
        //    //Console.WriteLine("2.   Hub/Chat: SendMessage(CreateMessageDto)");
        //    FunctionTracker.Instance().AddHubFunc("Hub/Chat: SendMessage(CreateMessageDto)");
        //    string userName = Context.User.GetUsername();
        //    Account sender = await repos.Accounts.GetByUsernameAsync(userName);

        //    var meetingGroup = await repos.Meetings.GetMeetingForConnection(Context.ConnectionId);

        //    if (meetingGroup != null)
        //    {
        //        MessageSignalrGetDto message = new MessageSignalrGetDto
        //        {
        //            SenderUsername = userName,
        //            SenderDisplayName = sender.Username,
        //            Content = createMessageDto.Content,
        //            MessageSent = DateTime.Now
        //        };
        //        //Luu message vao db
        //        //code here
        //        //send meaasge to meetingGroup
        //        await Clients.Group(meetingGroup.Id.ToString()).SendAsync("NewMessage", message);
        //    }
        //}
        #endregion
        //sẽ dc gọi khi FE gọi chatHubConnection.invoke('SendMessage', { content: string })
        public async Task SendMessage(MessageSignalrCreateDto createMessageDto)
        {
            FunctionTracker.Instance().AddHubFunc("Hub/Chat: SendMessage(CreateMessageDto)");
            string userName = Context.User.GetUsername();
            Account sender = await repos.Accounts.GetUserByUsernameSignalrAsync(userName);

            Meeting meeting = await repos.Meetings.GetMeetingForConnectionSignalr(Context.ConnectionId);

            if (meeting != null)
            {
                MessageSignalrGetDto message = new MessageSignalrGetDto
                {
                    SenderUsername = userName,
                    SenderDisplayName = sender.Username,
                    Content = createMessageDto.Content,
                    MessageSent = DateTime.Now
                };
                //Luu message vao db
                //code here
                //send meaasge to group
                await Clients.Group(meeting.Id.ToString()).SendAsync(NewMessageMsg, message);
            }
        }

        #region old code
        //public async Task MuteMicro(bool muteMicro)
        //{
        //    //Console.WriteLine("2.   " + new String('+', 50));
        //    //Console.WriteLine("2.   Hub/Chat: MuteMicro(bool)");
        //    FunctionTracker.Instance().AddHubFunc("Hub/Chat: MuteMicro(bool)");
        //    var meetingGroup = await repos.Meetings.GetMeetingForConnection(Context.ConnectionId);
        //    if (meetingGroup != null)
        //    {
        //        await Clients.Group(meetingGroup.Id.ToString()).SendAsync("OnMuteMicro", new { username = Context.User.GetUsername(), mute = muteMicro });
        //    }
        //    else
        //    {
        //        throw new HubException("meetingGroup == null");
        //    }
        //}
        #endregion
        //sẽ dc gọi khi FE gọi chatHubConnection.invoke('MuteMicro', mute)
        public async Task MuteMicro(bool muteMicro)
        {
            FunctionTracker.Instance().AddHubFunc("Hub/Chat: MuteMicro(bool)");
            Meeting meeting = await repos.Meetings.GetMeetingForConnectionSignalr(Context.ConnectionId);
            if (meeting != null)
            {
                await Clients.Group(meeting.Id.ToString()).SendAsync(OnMuteMicroMsg, new { username = Context.User.GetUsername(), mute = muteMicro });
            }
            else
            {
                throw new HubException("group == null");
            }
        }

        #region old region
        //public async Task MuteCamera(bool muteCamera)
        //{
        //    //Console.WriteLine("2.   " + new String('+', 50));
        //    //Console.WriteLine("2.   Hub/Chat: MuteCamera(bool)");
        //    FunctionTracker.Instance().AddHubFunc("Hub/Chat: MuteCamera(bool)");
        //    var meetingGroup = await repos.Meetings.GetMeetingForConnection(Context.ConnectionId);
        //    if (meetingGroup != null)
        //    {
        //        await Clients.Group(meetingGroup.Id.ToString()).SendAsync("OnMuteCamera", new { username = Context.User.GetUsername(), mute = muteCamera });
        //    }
        //    else
        //    {
        //        throw new HubException("meetingGroup == null");
        //    }
        //}
        #endregion
        //sẽ dc gọi khi FE gọi chatHubConnection.invoke('MuteCamera', mute)
        public async Task MuteCamera(bool muteCamera)
        {
            FunctionTracker.Instance().AddHubFunc("Hub/Chat: MuteCamera(bool)");
            Meeting meeting = await repos.Meetings.GetMeetingForConnectionSignalr(Context.ConnectionId);
            if (meeting != null)
            {
                await Clients.Group(meeting.Id.ToString()).SendAsync(OnMuteCameraMsg, new { username = Context.User.GetUsername(), mute = muteCamera });
            }
            else
            {
                throw new HubException("group == null");
            }
        }

        #region old code
        //private async Task<Meeting> RemoveConnectionFromGroup()
        //{
        //    //Console.WriteLine("2.   " + new String('+', 50));
        //    //Console.WriteLine("2.   Hub/Chat: RemoveConnectionFromGroup()");
        //    FunctionTracker.Instance().AddHubFunc("Hub/Chat: RemoveConnectionFromGroup()");
        //    Meeting group = await repos.Meetings.GetMeetingForConnection(Context.ConnectionId);
        //    Connection? connection = group.Connections.FirstOrDefault(x => x.Id == Context.ConnectionId);
        //    repos.Meetings.RemoveConnection(connection);

        //    return group;
        //}
        #endregion
        private async Task<Meeting> RemoveConnectionFromMeeting()
        {
            FunctionTracker.Instance().AddHubFunc("Hub/Chat: RemoveConnectionFromMeeting()");
            Meeting meeting = await repos.Meetings.GetMeetingForConnectionSignalr(Context.ConnectionId);
            Connection? connection = meeting.Connections.FirstOrDefault(x => x.Id == Context.ConnectionId);
            await repos.Meetings.EndConnectionSignalr(connection);
            IQueryable<Connection> activeConnections = repos.Meetings.GetActiveConnectionsForMeetingSignalr(meeting.Id);
            if(activeConnections.Count() == 0) 
            {
                await repos.Meetings.EndMeetingSignalRAsync(meeting);
            }
            return meeting;
        }

        #region old code
        //private async Task<Meeting> AddConnectionToMeeting(int meetingId)
        //{
        //    //Console.WriteLine("2.   " + new String('+', 50));
        //    //Console.WriteLine("2.   Hub/Chat: AddConnectionToMeeting(meetingIdString)");
        //    FunctionTracker.Instance().AddHubFunc("Hub/Chat: AddConnectionToMeeting(meetingIdString)");
        //    Meeting? meeting = await repos.Meetings.GetByIdAsync(meetingId);
        //    Connection connection = new Connection
        //    {
        //        Id = Context.ConnectionId,
        //        AccountId = Context.User.GetUserId()
        //    };
        //    if (meeting != null)
        //    {
        //        meeting.Connections.Add(connection);
        //    }

        //    return meeting;
        //}
        #endregion
        //TestOnly
        public async Task TestReceiveInvoke(string msg)
        {
            //int meetId = presenceTracker.
            Clients.Caller.SendAsync(OnTestReceiveInvokeMsg, "meehub invoke dc rồi ae ơi " + msg);
        }
    }
}
