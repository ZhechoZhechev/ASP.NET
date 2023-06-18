using Homies.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Homies.Models
{
    public class AllEventsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Organiser { get; set; } = null!;
        public DateTime Start { get; set; }

        public string Type { get; set; } = null!;

    }
}
