namespace Infrastructure.Marketplace;

public class MarketplaceRoutes
{
    public const string Home = "home";
    
    public static string PageSetting(string? pageSettingsId = null)
    {
        if (pageSettingsId == null)
        {
            return "page-setting";
        }
        else
        {
            return $"page-setting/{pageSettingsId}";
        }
    }
    
    public static string Faq(string? faqId = null)
    {
        if (faqId == null)
        {
            return "faq";
        }
        else
        {
            return $"faq/{faqId}";
        }
    }
    
    public static string Banner(string? id = null)
    {
        if (id == null)
        {
            return "banner";
        }
        else
        {
            return $"banner/{id}";
        }
    }

    public static string Social(string? id = null)
    {
        if (id == null)
        {
            return "social";
        }
        else
        {
            return $"social/{id}";
        }
    }

    public static string TextDynamic(string? id = null)
    {
        if (id == null)
        {
            return "text-dynamic";
        }
        else
        {
            return $"text-dynamic/{id}";
        }
    }

    public static string Category(string? categoryId = null)
    {
        if (categoryId == null)
        {
            return "category";
        }
        else
        {
            return $"category/{categoryId}";
        }
    }
    
    public static string RoleGold(string? roleGoldId = null)
    {
        if (roleGoldId == null)
        {
            return "role-gold";
        }
        else
        {
            return $"role-gold/{roleGoldId}";
        }
    }
    
    public static string RoleMoney(string? roleMoneyId = null)
    {
        if (roleMoneyId == null)
        {
            return "role-money";
        }
        else
        {
            return $"role-money/{roleMoneyId}";
        }
    }

    public static string TalaSootSettings(string? talaSootSettingsId = null)
    {
        if (talaSootSettingsId == null)
        {
            return "tala-sut-settings";
        }
        else
        {
            return $"tala-sut-settings/{talaSootSettingsId}";
        }
    }

    public static string IncomeSystem()
    {
        return "income-system";
    }
    
    public static string FinancialSectorValueManagement()
    {
        return "financial-sector-value-management";
    }
    
    public static string Branch(string? branchId = null)
    {
        if (branchId == null)
        {
            return "branch";
        }
        else
        {
            return $"branch/{branchId}";
        }
    }
    
    public static string Product(string? productId = null)
    {
        if (productId == null)
        {
            return "product";
        }
        else
        {
            return $"product/{productId}";
        }
    }
}
