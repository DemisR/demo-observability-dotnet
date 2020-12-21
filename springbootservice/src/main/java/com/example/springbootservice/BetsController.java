package com.example.springbootservice;

import io.micrometer.core.instrument.Counter;
import io.micrometer.core.instrument.Metrics;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.client.RestTemplate;

@RestController
public class BetsController {

    private Counter bets = Metrics.counter("bets");

    @Autowired
    private RestTemplate restTemplate;

    @GetMapping("/api/bets")
    public String bets() {
        bets.increment();
        return "We received your bets!";
    }

    @GetMapping("/api/chaining")
    public String chaining() {
        ResponseEntity<String> response = restTemplate.getForEntity("https://httpbin.org/status/200", String.class);
        return "Chaining + " + response.getBody();
    }

}
