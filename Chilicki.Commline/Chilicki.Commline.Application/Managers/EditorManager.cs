using Chilicki.Commline.Application.DTOs;

namespace Chilicki.Commline.Application.Managers
{
    public class EditorManager
    {
        readonly LineManager _lineManager;
        readonly StopManager _stopManager;
        readonly DepartureManager _departureManager;

        public EditorManager(
            LineManager lineManager,
            StopManager stopManager,
            DepartureManager departureManager)
        {
            _lineManager = lineManager;
            _stopManager = stopManager;
            _departureManager = departureManager;
        }

        public void EditStops(StopsEditionModel stopsEditionModel)
        {
            if (stopsEditionModel.Added != null)
                _stopManager.Create(stopsEditionModel.Added);
            if (stopsEditionModel.Modified != null)
                _stopManager.Edit(stopsEditionModel.Modified);
            if (stopsEditionModel.Removed != null)
                _stopManager.Remove(stopsEditionModel.Removed);
        }
    }
}
