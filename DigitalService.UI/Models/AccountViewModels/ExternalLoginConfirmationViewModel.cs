﻿using System.ComponentModel.DataAnnotations;

namespace DigitalService.UI.Models.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}