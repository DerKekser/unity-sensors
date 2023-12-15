# Unity - Sensors

[Sensors](https://github.com/DerKekser/unity-sensors) is a library for Unity that provides different 2D and 3D sensors.
The sensors are used to detect objects in the environment and can be used for example for AI.

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
Each sensor is available for 2D and 3D and can be used in the same way.

#### General Sensor Properties
| Property           | Type                   | Description                                                             |
|--------------------|------------------------|-------------------------------------------------------------------------|
| Auto Update Sensor | bool                   | If enabled, the sensor will be updated automatically.                   |
| Ignore             | Array of GameObjects   | The sensor ignores the specified objects.                               |
| Check Visibility   | bool                   | If enabled, the sensor checks whether the detected objects are visible. |
| Scan Layer         | LayerMask              | The layer of the objects that should be detected.                       |
| Obstruction Layer  | LayerMask              | The layer that obstructs the view if the sensor checks the visibility.  |
| Scan Rays          | Integer                | The number of rays that should be to check the visibility.              |
| Visibility         | Float (0-1)            | The Percentage of rays that must detect an object to be visible.        |
| OnEnter            | UnityEvent(GameObject) | The event that is called when an object enters the sensor.              |
| OnStay             | UnityEvent(GameObject) | The event that is called when an object stays in the sensor.            |
| OnExit             | UnityEvent(GameObject) | The event that is called when an object exits the sensor.               |

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

The RaySensor is a sensor that uses a single ray to detect objects.

| Property | Type  | Description               |
|----------|-------|---------------------------|
| Distance | Float |  The range of the sensor. |

![RaySensor](/Assets/Kekser/Screenshots/ray_sensor.png)

#### RangeSensor
`RangeSensor`
`RangeSensor2D`

The RangeSensor is a sensor that uses a sphere to detect objects.

| Property | Type  | Description              |
|----------|-------|--------------------------|
| Range    | Float | The range of the sensor. |

![RangeSensor](/Assets/Kekser/Screenshots/range_sensor.png)

#### ViewSensor
`ViewSensor`
`ViewSensor2D`

The ViewSensor is a sensor that uses a cone to detect objects.

| Property | Type  | Description               |
|----------|-------|---------------------------|
| Distance | Float |  The range of the sensor. |
| Angle    | Float | The angle of the sensor.  |

![ViewSensor](/Assets/Kekser/Screenshots/view_sensor.png)

#### TriggerSensor
`TriggerSensor`
`TriggerSensor2D`

The TriggerSensor is a sensor that uses a trigger collider to detect objects.
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