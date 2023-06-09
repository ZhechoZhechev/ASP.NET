﻿using System.ComponentModel.DataAnnotations;

namespace BusStation.Infrastructure.Data.Entities;

public class Destination
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string DestinationName { get; set; }

    [Required]
    [StringLength(50)]
    public string Origin { get; set; }

    public DateTime DateTime { get; set; }

    [Required]
    [StringLength(30)]
    public string Date { get; set; }

    [Required]
    [StringLength(30)]
    public string Time { get; set;}

    [Required]
    public string ImageUrl { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}

//⦁	Has Id – an int, Primary Key
//⦁	Has DestinationName – a string with min length 2 and max length 50 (required)
//⦁	Has Origin – a string with min length 2 and max length 50 (required)
//⦁	Has Date – a string with max length 30 (required). We recommend using "d" as a date format.The date cannot be smaller than the date of the creation of the destination.
//⦁	Has Time – a string with max length 30 (required). We recommend using "t" as a time format.
//⦁	Has ImageUrl – a string (required)
//⦁	Has Tickets collection

