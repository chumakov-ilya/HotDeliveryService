[![Build status](https://ci.appveyor.com/api/projects/status/mqkejkk5l7wgyxkr?svg=true)](https://ci.appveyor.com/project/chumakov-ilya/hotdeliveryservice)

Available data storages: Sqlite, Json (switch is in `Web.config`). 

REST API examples:

    GET /api/deliveries?status=available
    PUT /api/deliveries/1/actions/take?userId=2
