using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using Resources;

namespace DomainSeedworks;

public class BaseEntity : object
{
    public BaseEntity() : base()
    {
        Id = Guid.NewGuid().ToString();

        CreateDateTime = DateTime.Now;
        UpdateDateTime = DateTime.Now;

        IsActive = true;
        IsDeleted = false;
    }

    /// <summary>
    /// شناسه دیتابیس
    /// </summary>

    [Key]

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Guid))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string Id { get; private set; }
    
    protected void SetId(string? id)
    {
        if (id is null)
        {
            id = Guid.NewGuid().ToString();
        }
        
        Id = id;
    }

    /// <summary>
    /// تاریخ ایجاد
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CreateDate))]

    public DateTime CreateDateTime { get; private set; }

    /// <summary>
    /// تاریخ بروزرسانی
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.UpdateDate))]

    public DateTime UpdateDateTime { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Description))]

    public string? Description { get; set; }

    /// <summary>
    /// فعال بودن یا نبودن
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.IsActive))]

    public bool IsActive { get; set; }

    /// <summary>
    /// وضیعت حذف
    /// </summary>

    public bool IsDeleted { get; set; }

    /// <summary>
    /// ترتیب نمایش
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Order))]

    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    public int Ordering { get; set; }
    
    [NotMapped]
    public string ServerId { get; set; } = string.Empty;
    
    public static List<string> DoaminFinder(string assemblyName)
    {
        // Get the runtime libraries (assemblies) loaded in the project
        var dependencies = DependencyContext.Default.RuntimeLibraries;

        // Find the library that matches the provided assembly name
        var domainLibrary = 
            dependencies.FirstOrDefault(d => d.Name.Equals(assemblyName, StringComparison.OrdinalIgnoreCase));
        
        if (domainLibrary == null)
        {
            throw new ArgumentException($"Assembly '{assemblyName}' not found in the loaded dependencies.");
        }
     
        var assembly = Assembly.Load(new AssemblyName(assemblyName));
        
        if (assembly == null)
        {
            throw new ArgumentNullException(nameof(assembly), Messages.AssemblyCannotBeNull);
        }
        
        // find name list all domains
        var domainTypes = assembly.GetTypes()

            .Where(t => t.IsClass == true)
            
            .Where(t => t.IsAbstract == false)
            
            .Where(t => t.IsSubclassOf(typeof(BaseEntity)))
            
            .Select(t => t.Name)
            
            .ToList();

        return domainTypes;
    }
}