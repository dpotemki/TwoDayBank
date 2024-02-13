[🇺🇸 English](#english) | [🇷🇺 Русский](#русский)

## English

# TwoDayBank 🏦

## Description 📝

TwoDayBank is a demonstration project aimed at showcasing the practical application of Event Sourcing and Domain-Driven Design (DDD) in financial operations. This project includes a system for managing bank accounts and clients, enhanced with transaction notification capabilities  on platform c# DotNet.

## Infrastructure 🛠️

- **EventStore**: Centralized event storage, implemented using MS SQL or EventStoreDB.
- **MongoDB**: For storing data projections, facilitating quick access to the current state of accounts and clients.
- **Kafka**: Messaging backbone for inter-service communication.
- **Zookeeper**: Manages Kafka cluster coordination.
- **AKHQ**: Kafka queue management and monitoring tool.
- **Mongo-express**: Web-based MongoDB data management interface.
- **Loki & Grafana**: Logging and visualization duo for comprehensive system monitoring.
- **Kubernetes**: Ensures scalability and orchestration of services.

![Infrastructure Diagram](assets/TwoDayBankInfrastructure.svg)

## Components 📦

- **TwoDayDemoBank.Service.Core**: Primary service for account and client operations, handling REST requests, event creation, and projection management.
- **TwoDayDemoBank.Worker.Core**: Subscribes to Kafka events, responsible for building and updating MongoDB projections based on events like `TransactionHappened` and `AccountCreated`.
- **TwoDayDemoBank.Worker.Notifications**: Processes events to dispatch notifications, leveraging Service Core data through HTTP requests for essential information.

## Architectural Features 🏗️

The project leverages Event Sourcing and DDD principles to maintain data integrity and streamline business logic. Clean architecture aids in separating concerns and simplifying component testing.

## Testing 🔍

Extensive testing, from unit to integration tests, ensures the project's quality and reliability.

## Getting Started 🚀

Deploy the infrastructure using `docker-compose.yml`. Start TwoDayDemoBank services with the `run_projects.bat` script.

## Project Goal 🎯

This project is designed for those interested in hands-on experience with Event Sourcing, DDD, and event-driven systems, demonstrating their application in a real-world scenario and providing a foundation for further exploration and learning.

---

## Русский

# TwoDayBank 🏦

## Описание 📝

TwoDayBank — это демонстрационный проект, цель которого — показать практическое применение Event Sourcing и Domain-Driven Design (DDD) в финансовых операциях. Проект включает в себя систему управления банковскими счетами и клиентами с возможностью уведомления о транзакциях на платформе c# DotNet.

## Инфраструктура 🛠️

- **EventStore**: Централизованное хранилище событий, реализованное на основе MS SQL или EventStoreDB.
- **MongoDB**: Для хранения проекций данных, облегчая доступ к текущему состоянию счетов и клиентов.
- **Kafka**: Основа для обмена сообщениями между сервисами.
- **Zookeeper**: Управление координацией кластера Kafka.
- **AKHQ**: Инструмент для управления и мониторинга очередей Kafka.
- **Mongo-express**: Веб-интерфейс для управления данными MongoDB.
- **Loki & Grafana**: Дуэт для логирования и визуализации, обеспечивающий комплексный мониторинг системы.
- **Kubernetes**: Обеспечивает масштабируемость и оркестрацию сервисов.

![Infrastructure Diagram](assets/TwoDayBankInfrastructure.svg)

## Компоненты 📦

- **TwoDayDemoBank.Service.Core**: Основной сервис для операций с аккаунтами и клиентами, обрабатывает REST-запросы, создает события и управляет проекциями данных.
- **TwoDayDemoBank.Worker.Core**: Подписывается на события Kafka, отвечает за построение и обновление проекций в MongoDB на основе событий, таких как `TransactionHappened` и `AccountCreated`.
- **TwoDayDemoBank.Worker.Notifications**: Обрабатывает события для отправки уведомлений, используя данные из Service Core через HTTP-запросы для получения необходимой информации.

## Архитектурные особенности 🏗️

Проект использует принципы Event Sourcing и DDD для поддержания целостности данных и упрощения бизнес-логики. Чистая архитектура способствует разделению ответственности и упрощению тестирования компонентов.

## Тестирование 🔍

Обширное тестирование, от модульных до интеграционных тестов, гарантирует качество и надежность проекта.

## Начало работы 🚀

Разверните инфраструктуру с помощью `docker-compose.yml`. Для запуска сервисов TwoDayDemoBank используйте скрипт `run_projects.bat`.

## Цель проекта 🎯

Проект создан для тех, кто хочет получить практический опыт работы с Event Sourcing, DDD и событийно-ориентированными системами, демонстрируя их применение в реальных условиях и предоставляя основу для дальнейшего изучения и экспериментов.
