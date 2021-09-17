using CartoPrime.Application.Interfaces;
using CartoPrime.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartolaPrime.Web.Pages
{
    public class IndexBModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBonsDeBicoApplication _pageApplication;
        public IEnumerable<MatchBonsDeBico> MatchBons { get; set; }

        public IndexBModel(ILogger<IndexModel> logger, IBonsDeBicoApplication PageApplication)
        {
            _logger = logger;
            this._pageApplication = PageApplication;
            MatchBons = _pageApplication.ListMatchsB().Result.Data;
        }

        public void OnGet()
        {

        }
    }
}
