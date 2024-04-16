using Microsoft.AspNetCore.Components;

namespace TheST.Presentation.Components
{
    public class BasicComponent : ComponentBase
    {
        [Parameter]
        public virtual string? Class { get; set; }

        [Parameter]
        public virtual string? Style { get; set; }
    }
}
