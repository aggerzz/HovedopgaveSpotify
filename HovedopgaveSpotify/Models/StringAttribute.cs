﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HovedopgaveSpotify.Models
{
    public class StringAttribute : Attribute
    {
        public String Text { get; set; }
        public StringAttribute(String text)
        {
            this.Text = text;
        }
    }
}