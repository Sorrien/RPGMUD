﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MUDService.Models
{
    public class Player : Character
    {

        public List<ChatUser> ChatUsers { get; set; }
    }
}