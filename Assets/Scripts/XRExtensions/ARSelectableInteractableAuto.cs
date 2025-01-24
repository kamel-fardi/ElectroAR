using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;


public class ARSelectableInteractableAuto : ARSelectionInteractable
{
    public bool m_GestureSelected { get; private set; } = true;
    public override bool IsSelectableBy(IXRSelectInteractor interactor) => interactor is ARGestureInteractor && m_GestureSelected;
    protected override bool CanStartManipulationForGesture(TapGesture gesture) => true;

    /// <inheritdoc />
    protected override void OnEndManipulation(TapGesture gesture)
    {
        base.OnEndManipulation(gesture);

        if (gesture.isCanceled)
            return;
        if (gestureInteractor == null)
            return;

        if (gesture.targetObject == gameObject)
        {
            // Toggle selection
            m_GestureSelected = !m_GestureSelected;
        }
        else
            m_GestureSelected = false;
    }
}
