# Run

`docker-compose up -d --build`


For build and run reac without docker :
```
cd react/ 
npm run deploy
```

# URLs:

dotnetservice :
- http://localhost:5000/api/values
- actuator
    - http://localhost:5000/actuator/health
    - http://localhost:5000/actuator/info
    - http://localhost:5000/actuator/prometheus

Prometheus : http://localhost:9090
Grafana : http://localhost:3000 (user: admin / pass: secret) 
Zipkin: http://localhost:9411/zipkin/?serviceName=dotnetservice
Sentry: https://sentry.io/organizations/demo-lm/issues/?project=5563890
React : http://localhost:3005

# Setup

## Setup logs Docker to Loki

```
docker plugin install grafana/loki-docker-driver:latest --alias loki --grant-all-permissions
docker plugin ls
docker plugin enable loki
```

### Configure
https://github.com/grafana/loki/blob/master/docs/clients/docker-driver/configuration.md

specify driver in docker-compose service 
```yaml
    logging:
      driver: loki
      options:
        loki-url: "http://localhost:3100/loki/api/v1/push"
```

### To remove

```
docker plugin disable loki
docker plugin rm loki
```

---

# Test

```
while true; do sleep 1; curl http://localhost:5000/api/values; done
```


Got to react front 
select item add to cart and checkout
...Something went wrong
but how found the problem ?

Go to sentry ent check the problem
Got to grafana and check errors 500 with loki `{compose_service="dotnetservice"} |~ "500"`
Click on the log and open the trace