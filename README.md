# CPER-G5

## SIMULATOR

//TODO

### START

//TODO

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
