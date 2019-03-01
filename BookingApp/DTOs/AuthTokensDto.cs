﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class AuthTokensDto
    {
        public string AccessToken { get; set; }

        public override bool Equals(object obj)
        {
            return AccessToken == ((AuthTokensDto)obj).AccessToken;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AccessToken);
        }
    }
}
