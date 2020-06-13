using System;
using System.ComponentModel.DataAnnotations;

namespace Reactivities.Domain
{
    //This will be join table for user with activity
    public class UserActivity
    {
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        [MaxLength(255)]
        public Guid ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
        public DateTime DateJoined { get; set; }
        public bool IsHost { get; set; }
    }
}