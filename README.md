# CPER-G5

## SIMULATOR

### OPTIONS OF CONFIG FILE
The configuration file is in `/Config/configuration.json` and have these value to customise:

1. **Heartbeat**
   - `Max` Represents the maximum generation value (including errors), default `300`
   - `Min` Represents the minimun generation value (including error), default `0`
   - `HighLimit` Represents the maximun normal generation value, default `200`
   - `LowLimit` Represents the minumun normal generation value, default `30`
   - `ErrorRate` Represents the rate (in %) of error generation, default `20`
   - `MaxIncrease` Represents the maximum increase value, default `5`
   - `ErrorRate` Represents the maximum decrease value, default `-5`
   
2. **Position**
   - `ErrorRate` Represents the rate (in %) of error generation, default `20`
   
3. **Lap**
   - `Min` Represents the minimun generation value (in sec), default `40`
   - `Max` Represents the maximun generation value (in sec), default `85`
   - `LapsCount` Represents every how many data generation laps increase, default `3`
   
4. **PoolLenghts**
   - `Array` Represents the Lenghts of the swimming pool, default `[25, 50]`


## API

### DOCUMENTATION

**POST /writeData**
   - `Guid IdDevice` Represents the GUID of the device that the data is from
   - `Guid IdActivity` Represents the GUID of the activity that the data is from
   - `int Heartbeat` Represents the value of the heartbeat, out of range `<20 || 200>`
   - `Postion Position` Represents the geografical position expressed in coordinates, object formed by
      - `Longitude` X geografical coordinate, out of range `<-180 || >180`
      - `Latitude` Y geografical coordinate, out of range `<-90 || >90`
   - `int Laps` Represents the number of laps done during the current activity
   - `DateTime Time` Represent the time of the measurement  

**GET /getActivities**
- PARAMETERS:
   - `string devGUID` Represents the GUID of the device that the data is from
- RETURNS:
   List of:
   - `Guid? IdDevice` Represents the GUID of the device that the data is requested
   - `DateTime Time` Represent the time of the measurement
   - `int Duration` Represent, in minutes, the duration of the activity
   - `int AvgHB` Represent the average heartbeat during the activity
   - `Postion Position` Represents the geografical position expressed in coordinates, object formed by
      - `Longitude` X geografical coordinate, out of range
      - `Latitude` Y geografical coordinate
   - `int Laps` Represents the number of laps done during the activity
      
**GET /getRows**
- PARAMETERS:
   - `string devGUID` Represents the GUID of the device that the data is from
   - `string actGUID` Represents the GUID of the activity that the data is from
- RETURNS:
   - `DateTime Date` Represent the dateTime of the first data in the selected activity
   - `int Duration` Represent, in minutes, the duration of the activity
   - `Postion Position` Represents the geografical position expressed in coordinates, object formed by
      - `Longitude` X geografical coordinate, out of range
      - `Latitude` Y geografical coordinate
   - `int Laps` Represents the number of laps done during the activity
   - List of: `HBInstance` Represent the single instance of an hartbeat measurement, object formed by:
      - `long HeartBeat` Represents the value of the heartbeat
      - `Datetime Time` Represents the time when the value was measured
            
**GET /getAvgHB**
- PARAMETERS:
  - `string devGUID` Represents the GUID of the device that the data is from
- RETURNS:
  - `int HeartBeat` Represents the average value of the heartbeat of a device

**GET /getAvgLaps**
- PARAMETERS:
  - `string devGUID` Represents the GUID of the device that the data is from
- RETURNS:
  - `int Laps` Represents the average value of the laps of a device

**GET /getErrors**
- RETURNS
  - `string devGUID` Represents the GUID of the device that the data comes from
  - `string actGUID` Represents the GUID of the activity that the data comes from
  - `string field` Represents the name of the field
  - `string data` Represents the date that resulted in an error
   
