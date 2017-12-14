﻿using AetheriaWebService.Hubs;
using AetheriaWebService.Models;
using AetheriaWebService.ServiceModels;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AetheriaWebService.Helpers
{
    public interface IReplicationHelper
    {
        void ReplicateToClients(string message, List<ChatUser> chatUsers);
    }
    public class ReplicationHelper : IReplicationHelper
    {
        private readonly IHubContext<AetheriaHub> _messageHubContext;

        public ReplicationHelper(IHubContext<AetheriaHub> messageHubContext)
        {
            _messageHubContext = messageHubContext;
        }

        public async void ReplicateToClients(string message, List<ChatUser> chatUsers)
        {
            var relevantChatUsers = new List<ChatUserDTO>();
            foreach (var chatUser in chatUsers)
            {
                relevantChatUsers.Add(new ChatUserDTO
                {
                    ChatUserId = chatUser.UserId,
                    Platform = chatUser.Platform
                });
            }
            var serverResponse = new AetheriaServerResponse
            {
                ServerAuthToken = "TestToken",
                RelevantChatUsers = relevantChatUsers,
                Response = message
            };
            var responseMessage = JsonConvert.SerializeObject(serverResponse);

            await _messageHubContext.Clients.All.InvokeAsync("Send", responseMessage);
        }
    }
}