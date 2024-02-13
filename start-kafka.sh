#!/bin/bash

# Запуск Kafka
/etc/confluent/docker/run &

# Ждём запуска Kafka
sleep 10

# Создание топика
kafka-topics --create --if-not-exists --bootstrap-server localhost:9092 --topic events --partitions 1 --replication-factor 1

# Ожидание завершения Kafka
wait
