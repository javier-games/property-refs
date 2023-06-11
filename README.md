# PropertyRefs for Unity

PropertyRefs is a comprehensive tool for Unity, crafted to streamline your game development tasks. By creating references to the properties of the components, you can efficiently access and manipulate them from other scripts, centralizing meaningful properties of a game object, prefab, or a scene in one location. This is a significant advantage for game designers and level designers, offering an enhanced and seamless workflow.

![Property Refs Sample](Documentation~/Images/property-refs-sample.gif)

## Features

### Access from Editor

With a custom property drawer, PropertyRefs provides an intuitive interface for gathering all significant properties in one place. This not only eliminates the tedious search through countless properties but also promotes a cleaner, more organized codebase.

### Access from Code

Developers have the flexibility to modify the values of the PropertyRefs programmatically, introducing a new level of adaptability to your Unity projects. For more information on how to access and modify properties programmatically, please refer to our [Usage Section](#usage). 

### AOT Systems Support

PropertyRefs is equipped with a dual-mode functionality. Initially, it uses System Reflection to access the values of component properties. However, for AOT systems, where System Reflection is not applicable, PropertyRefs seamlessly switches to a Roslyn Source Generator. For more details on how AOT systems are supported, please see our [Usage Section](#usage).

## Installation

There are several methods available to install PropertyRefs into your Unity project. Choose the one that best suits your needs. It's recommended to install via npm if you wish to keep the package up-to-date easily, as future releases and updates will be readily available.

### Installation via npm

To add PropertyRefs from a npm registry in Unity, follow the steps below:

1. Open your Unity project and navigate to `Edit > Project Settings > Package Manager`.
2. In the `Scoped Registries` section, click on the `+` button to add a new scoped registry.
3. Fill in the required fields:
    - Name: Enter `Monogum`.
    - URL: Enter `https://registry.npmjs.org`.
    - Scope(s): Enter `games.monogum.bricksbucket`.
4. Click `Save` and close the Project Settings window.
5. Navigate to `Window > Package Manager`.
6. In the package manager window, select `Packages: My Registries`.
7. You should see the `games.monogum.bricksbucket.propertyrefs` package listed there. Click `Install` to install the package.

Alternatively, you can directly modify your `Packages/manifest.json` file to add the new scoped registry and the dependency:

1. Add the following scope to the `scopedRegistries` in your `manifest.json` file:
```json
{
  "scopedRegistries": [
    {
      "name": "Monogum",
      "url": "https://registry.npmjs.org",
      "scopes": ["games.monogum.bricksbucket"]
    }
  ]
}
```
2.	Then, add `games.monogum.bricksbucket.propertyrefs` to the dependencies section of your `manifest.json` file:
```json
{
  "dependencies": {
    "games.monogum.bricksbucket.propertyrefs": "1.0.0"
  }
}
```
Ensure to replace 1.0.0 with the version number of the PropertyRefs package you intend to install. Save your manifest.json file after making these changes.


### Installation via tarball

To install PropertyRefs from a tarball in Unity, follow these steps:

1. Download the tarball file (`games.monogum.bricksbucket.propertyrefs.{version}.tgz`) from the releases section of this repository.
2. Open your Unity project and navigate to `Window > Package Manager`.
3. In the Package Manager window, click on the `+` icon in the top-left corner.
4. From the dropdown, select `Add package from tarball...`.
5. A file explorer window will open. Navigate to the location where you saved the downloaded tarball file.
6. Select the tarball file and click `Open`.

Unity will automatically install the package from the tarball file. The package should now be listed in the Package Manager.

### Installation via Cloning

To install PropertyRefs by cloning the repository and adding the package from disk, follow these steps:

1. Clone this repository to your local machine.
```shell
git clone https://github.com/javier-games/property-refs.git
```
2. Rename the folder `Samples` to `Samples~`.
```shell
mv Samples Samples\~
```
3. Open your Unity project and navigate to `Window > Package Manager`.
4. In the Package Manager window, click on the `+` icon in the top-left corner.
5. From the dropdown, select `Add package from disk...`. 
6. A file explorer window will open. Navigate to the location where you cloned the repository.
7. Find and select the `package.json` file within the cloned repository and click `Open`.

Unity will automatically detect and install the package. The package should now be listed in the Package Manager.



## Usage

![Property Refs Sample List](Documentation~/Images/property-refs-sample-list.png)

PropertyRefs is designed with user-friendliness in mind, intending to enhance your Unity development experience. It simplifies your process of accessing and modifying component properties, making it an indispensable tool for any Unity developer.

```csharp
using System.Collections.Generic;
using UnityEngine;
using Monogum.BricksBucket.PropertyRefs;

public class PrefabProperties : MonoBehaviour
{

   [SerializeField]
   private PropertyRef propertyRef;
   
   [SerializeField]
   private List<PropertyRef> properties;
}
```

### Code

```csharp
using UnityEngine;
using Monogum.BricksBucket.PropertyRefs;

public class OrcController : MonoBehaviour
{
	[SerializeField]
	private PropertyRef propertyRef;

	private void Start()
	{
		var value = (float) propertyRef.GetValue();
		Debug.Log($"My value is {value}");

		value += 5;
		propertyRef.SetValue(value);
		Debug.Log($"My new value is {value}");
	}
}
```

### AOT Support
```json
{
   "components": [
      {
         "type": "UnityEngine.Transform", 
         "properties": [
            {
               "name": "position", 
               "type": "UnityEngine.Vector3"
            }
         ]
      }
   ]
}
```
## Donations

PropertyRefs is a open source project, and it's because of your support that we can stay up and running. If you find this project useful, please consider [making a donation](https://www.paypal.com/donate/?hosted_button_id=QY4PCGA8FMCC4). Your contribution will help us to maintain the project, and continue to develop new features. We appreciate your generosity!

## Contribution

Please read our [Contributing Guide](CONTRIBUTING.md) before submitting a Pull Request to the project.

## Support

For any questions or issues, please open a new issue on this repository.

## License

PropertyRefs is available under the MIT license. See the [LICENSE](LICENSE) file for more info.
