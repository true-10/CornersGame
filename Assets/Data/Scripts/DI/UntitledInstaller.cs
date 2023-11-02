using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "UntitledInstaller", menuName = "Installers/UntitledInstaller")]
public class UntitledInstaller : ScriptableObjectInstaller<UntitledInstaller>
{
    public override void InstallBindings()
    {
    }
}