using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Train_Reservation_Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeatureFlagsController : ControllerBase
    {
        private readonly IFeatureManager _featureManager;

        public FeatureFlagsController(IFeatureManager featureManager)
        {
            _featureManager = featureManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetFeatureFlags()
        {
            var enabledFeatureFlags = new List<string>();
            await foreach (var name in _featureManager.GetFeatureNamesAsync())
            {
                if (await _featureManager.IsEnabledAsync(name))
                {
                    enabledFeatureFlags.Add(name);
                }
            }
            return Ok(enabledFeatureFlags);
        }
    }
}
