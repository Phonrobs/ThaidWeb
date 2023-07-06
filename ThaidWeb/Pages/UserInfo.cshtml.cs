using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Specialized;
using ThaidWeb.Services.Thaid;

namespace ThaidWeb.Pages;

public class UserInfoModel : PageModel
{
    public NameValueCollection TokenList { get; set; }
    //public NameValueCollection IdTokenList { get; set; }


    private readonly IThaidService _thaidService;

    public UserInfoModel(IThaidService thaidService)
    {
        _thaidService = thaidService;
    }

    public async Task OnGet(string key)
    {
        var token = await _thaidService.GetTokenFromCacheAsync(key);

        var tokenProps = token.GetType().GetProperties();

        TokenList = new();

        foreach (var prop in tokenProps)
        {
            //if (prop.Name == "IdToken") continue;
            var pv = prop.GetValue(token, null);

            if (pv != null)
            {
                TokenList.Add(prop.Name, pv.ToString());
            }
            else
            {
                TokenList.Add(prop.Name, "NULL");
            }
        }

        //if (token.IdToken == null) return;

        //var idTokenProps = token.IdToken.GetType().GetProperties();

        //foreach (var prop in idTokenProps)
        //{
        //    IdTokenList.Add(prop.Name, prop.GetValue(token.IdToken, null).ToString());
        //}
    }
}
