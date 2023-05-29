﻿using API.SignalRHub.Tracker;
using APIExtension.ClaimsPrinciple;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ShareResource.DTO.Connection;

namespace API.SignalRHub
{
    [Authorize]
    public class PresenceHub : Hub
    {
        private readonly PresenceTracker _tracker;
        public PresenceHub(PresenceTracker tracker)
        {
            _tracker = tracker;
        }
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("3.      " + new string('+', 50));
            Console.WriteLine("3.      Hub/Presence: OnConnectedAsync()");
            FunctionTracker.Instance().AddHubFunc("3.      Hub/Presence: OnConnectedAsync()");
            var isOnline = await _tracker.UserConnected(new UserConnectionDto(Context.User.GetUsername(), 0), Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("3.      " + new string('+', 50));
            Console.WriteLine("3.      Hub/Presence: OnDisconnectedAsync(Exception)");
            FunctionTracker.Instance().AddHubFunc("3.      Hub/Presence: OnDisconnectedAsync(Exception)");
            var isOffline = await _tracker.UserDisconnected(new UserConnectionDto(Context.User.GetUsername(), 0), Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
