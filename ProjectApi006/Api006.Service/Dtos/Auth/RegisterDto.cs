﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api006.Service.Dtos
{
    public class RegisterDto
    {
        public string UserName {  get; set; }
        [EmailAddress]
        public string Email {  get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword {  get; set; }
    }
}
