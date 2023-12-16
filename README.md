# Unity - Sensors

[Sensors](https://github.com/DerKekser/unity-sensors) is a Unity library providing various 2D and 3D sensors to detect objects within the game environment. These sensors can be particularly useful for AI development.

## Contents
- [Sensor Types](#sensor-types)
  - [General Sensor Properties](#general-sensor-properties)
  - [Access the sensor data](#access-the-sensor-data)
  - [Manual Update](#manual-update)
  - [RaySensor](#raysensor)
  - [RangeSensor](#rangesensor)
  - [ViewSensor](#viewsensor)
  - [TriggerSensor](#triggersensor)
- [Install](#install)
  - [Install via Unity Package](#install-via-unity-package)
  - [Install via git URL](#install-via-git-url)
- [License](#license)

### Sensor Types
Each sensor type is available in both 2D and 3D and operates similarly

#### General Sensor Properties
| Property           | Type                   | Description                                                   |
|--------------------|------------------------|---------------------------------------------------------------|
| Auto Update Sensor | bool                   | Enables automatic sensor updates.                             |
| Ignore             | GameObject[]           | Ignores specified objects.                                    |
| Check Visibility   | bool                   | Checks if detected objects are visible.                       |
| Scan Layer         | LayerMask              | Defines the layer of detectable objects.                      |
| Obstruction Layer  | LayerMask              | Specifies the layer obstructing visibility checks.            |
| Scan Rays          | int                    | Number of rays used for visibility checks.                    |
| Visibility         | float (0-1)            | Percentage of rays needed to detect an object for visibility. |
| OnEnter            | UnityEvent(GameObject) | Triggered when an object enters the sensor's range.           |
| OnStay             | UnityEvent(GameObject) | Triggered when an object stays within the sensor's range.     |
| OnExit             | UnityEvent(GameObject) | Triggered when an object exits the sensor's range.            |

#### Access the sensor data
```csharp
RaySensor sensor = GetComponent<RaySensor>();
GameObject[] detectedObjects = sensor.DetectedObjects;
```
#### Manual Update
```csharp
RaySensor sensor = GetComponent<RaySensor>();
sensor.SensorUpdate();
```
#### RaySensor
`RaySensor`
`RaySensor2D`

Uses a single ray to detect objects.

| Property | Type  | Description          |
|----------|-------|----------------------|
| Distance | Float | Range of the sensor. |

![RaySensor](/Assets/Kekser/Screenshots/ray_sensor.png)

#### RangeSensor
`RangeSensor`
`RangeSensor2D`

Uses a sphere to detect objects.

| Property | Type  | Description          |
|----------|-------|----------------------|
| Range    | Float | Range of the sensor. |

![RangeSensor](/Assets/Kekser/Screenshots/range_sensor.png)

#### ViewSensor
`ViewSensor`
`ViewSensor2D`

Uses a cone to detect objects.

| Property | Type  | Description          |
|----------|-------|----------------------|
| Distance | Float | Range of the sensor. |
| Angle    | Float | Angle of the sensor. |

![ViewSensor](/Assets/Kekser/Screenshots/view_sensor.png)

#### TriggerSensor
`TriggerSensor`
`TriggerSensor2D`

Uses a trigger collider to detect objects.
The trigger collider component must be attached to the same GameObject as the sensor.

![TriggerSensor](/Assets/Kekser/Screenshots/trigger_sensor.png)

### Install

#### Install via Unity Package

Download the latest [release](https://github.com/DerKekser/unity-sensors/releases) and import the package into your Unity project.
#### Install via git URL

```
https://github.com/DerKekser/unity-sensors.git?path=Assets/Kekser/Sensors
```
![Package Manager](/Assets/Kekser/Screenshots/package_manager.png)
### License

This library is under the MIT License.