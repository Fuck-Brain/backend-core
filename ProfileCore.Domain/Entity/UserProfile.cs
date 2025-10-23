using ProfileCore.Domain.DomainAbstract;

namespace ProfileCore.Domain.Entity;

public class UserProfile : BaseEntity, IAuditable
{
    public string DisplayName { get; private set; }
    public string? Name { get; private set; }
    public string? Surname { get; private set; }
    public string? FatherName { get; private set; }
    public string? Bio { get; private set; }

    private UserProfile(Guid id, string displayName, string name, string surname, string fatherName, string bio)
    {
        Id = id;
        this.DisplayName = displayName;
        this.Name = name;
        this.Surname = surname;
        this.FatherName = fatherName;
        this.Bio = bio;
    }

    public static UserProfile Create(Guid id, string displayName)
    {
        return new UserProfile(
            id, 
            displayName, 
            "", 
            "", 
            "", 
            ""
            );
    }
    
    public void UpdateName(string newName) => this.Name = newName;
    public void UpdateDisplayName(string newDisplayName) => this.DisplayName = newDisplayName;
    public void UpdateSurname(string newSurname) => this.Surname = newSurname;
    public void UpdateFatherName(string newFatherName) => this.FatherName = newFatherName;
    public void UpdateBio(string newBio) => this.Bio = newBio;
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}