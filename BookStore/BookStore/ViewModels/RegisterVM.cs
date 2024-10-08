﻿using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels
{
    public class RegisterVM
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string UserName {  get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword {  get; set; }
        [EmailAddress]
        public string Email {  get; set; }
        public bool IsTerm {  get; set; }
    }
}
