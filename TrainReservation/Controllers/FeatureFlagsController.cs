using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrainReservation.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)]
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
            var enabledFeatureFlags = new Dictionary<string, bool>();
            await foreach (var name in _featureManager.GetFeatureNamesAsync())
            {
                if (await _featureManager.IsEnabledAsync(name))
                {
                    enabledFeatureFlags.Add(name, true);
                }
                else
                {
                    enabledFeatureFlags.Add(name, false);
                }
            }
            return Ok(enabledFeatureFlags);
        }
    }
}
