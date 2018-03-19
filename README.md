# Linz Linien EFA
This project serves a wrapper around the public transit data API of the city Linz.

## Description
The main goal of this wrapper API is to offer an API which follows the RESTful API pattern and only returns the essential information from the live public transit API.

The Web API project contains two controllers for two endpoints respectively.

### Stops Endpoint
```
api/stops/{name}
```  
Returns a list of stops found with the name `{name}`. The underlying API not only searches stops which contain the string passed as an URL segment, but also partially matching names. I.e. misspelled names return the correct stops.

**Example Request**  
`/api/stops/taubenmarkt`

**Example Response**. 
```
[  
  {  
    "id":"60501160",
    "name":"Taubenmarkt"
  }
]
```

### Departures Endpoint 
```
api/departures/{stopId}
```  
Returns a list of the next 40 (default from base API) departures from the given a stop ID in all directions.

**Example Request**  
`/api/departures/60501160`

**Example Response**. 
```
[  
  {  
    "countdownInMinutes":3,
    "time":"2018-03-19T20:49:00",
    "line":{  
      "number":"2",
      "type":4,
      "direction":"solarCity",
      "initialOriginStopName":"Linz JKU I Universität"
    }
  },
  {  
    "countdownInMinutes":8,
    "time":"2018-03-19T20:54:00",
    "line":{  
      "number":"1",
      "type":4,
      "direction":"Universität",
      "initialOriginStopName":"Linz Auwiesen"
    }
  },
  ...
]
```

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgments
* The public transport [data](https://www.data.gv.at/katalog/dataset/9faa1734-607f-4bfd-b8c9-c5692bf37d55) by the city Linz is licensed under [CC BY 3.0 AT](https://creativecommons.org/licenses/by/3.0/at/deed.en).
