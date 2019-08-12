using System;

namespace OAuthTest.Models
{
    public interface ITrackable
    {
        DateTime? createdAt { get; set; }
        DateTime? updatedAt { get; set; }
    }
}