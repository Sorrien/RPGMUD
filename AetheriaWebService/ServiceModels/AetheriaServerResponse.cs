﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AetheriaWebService.ServiceModels
{
    public class AetheriaServerResponse
    {
        public string ServerAuthToken { get; set; }
        public List<ChatUserDTO> RelevantChatUsers { get; set; }
        public string Response { get; set; }
    }

    public class ChatUserDTO
    {
        public string Platform { get; set; }
        public string ChatUserId { get; set; }
    }
}