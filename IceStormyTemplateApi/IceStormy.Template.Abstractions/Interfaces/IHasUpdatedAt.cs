namespace IceStormy.Template.Abstractions.Interfaces;

/// <summary>
/// Represents an entity that has an update date and time.
/// </summary>
public interface IHasUpdatedAt
{
    /// <summary>
    /// Date and time of the entity change
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}