using ShareResource.DTO.Connection;

namespace API.SignalRHub.Tracker
{
    public class ShareScreenTracker
    {
        // chứa xem user ở meeting nào đang shareScreen
        public static readonly List<UserConnectionSignalrDto> usersSharingScreen = new List<UserConnectionSignalrDto>();

        public Task<bool> AddUserSharingScreen(UserConnectionSignalrDto userMeetConnection)
        {
            bool isOnline = false;
            lock (usersSharingScreen)
            {
                UserConnectionSignalrDto exsited = usersSharingScreen.FirstOrDefault(x => x.Username == userMeetConnection.Username && x.MeetingId == userMeetConnection.MeetingId);

                if (exsited == null)//chua co online
                {
                    usersSharingScreen.Add(userMeetConnection);
                    isOnline = true;
                }
            }
            return Task.FromResult(isOnline);
        }

        public Task<bool> RemoveUserShareScreen(UserConnectionSignalrDto userMeetConnection)
        {
            bool isOffline = false;
            lock (usersSharingScreen)
            {
                var temp = usersSharingScreen.FirstOrDefault(x => x.Username == userMeetConnection.Username && x.MeetingId == userMeetConnection.MeetingId);
                if (temp == null)
                    return Task.FromResult(isOffline);
                else
                {
                    usersSharingScreen.Remove(temp);
                    isOffline = true;
                }
            }
            return Task.FromResult(isOffline);
        }

        public Task<bool> RemoveUserShareScreen(string username, int meetingId)
        {   bool isOffline = false;
            lock (usersSharingScreen)
            {
                var temp = usersSharingScreen.FirstOrDefault(x => x.Username == username && x.MeetingId == meetingId);
                if (temp != null)
                {
                    isOffline = true;
                    usersSharingScreen.Remove(temp);
                }
            }
            return Task.FromResult(isOffline);
        }

        public Task<UserConnectionSignalrDto> GetUserIsSharingScreenForMeeting(int meetingId)
        {  
            UserConnectionSignalrDto user = null;
            lock (usersSharingScreen)
            {
                user = usersSharingScreen.FirstOrDefault(x => x.MeetingId == meetingId);                               
            }
            return Task.FromResult(user);
        }
    }
}
