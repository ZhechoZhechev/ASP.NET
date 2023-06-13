﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace Contacts.Data.Entities;

public class ApplicationUserContact
{
    [Required]
    [ForeignKey(nameof(ApplicationUser))]
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Contact))]
    public int ContactId { get; set; }
    public Contact Contact { get; set; } = null!;

}
