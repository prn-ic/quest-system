# QuestSystem

#### Тестовое задание на позицию .NET Developer: Разработка REST API для системы квестов в игре

## Содержание
- Описание
- Как запустить?
- Инструкция по использованию

## Описание

Необходимо разработать REST API для системы управления квестами в игре. Система должна позволять игрокам:
Просматривать доступные квесты.
Принимать квесты.
Обновлять прогресс выполнения квестов.
Завершать квесты и получать награду.

## Как запустить?
Для того, чтобы запустить проект, достаточно, в корневой директории, прописать <br/>
``` docker compose -f deploy/compose-dev.yaml up -d``` <br/>
В целях простоты, не стал убирать файл переменных окружения в gitignore, но на будущее все же стоит)

## Инструкция по использованию
По умолчанию, при первом запуске приложения, создается пользователь с такими данными:

``` 
Id = "a8587ff3-432c-4d91-920e-d1d50c07558e"
Name = "Oleg"
MinimumLevel = 0
```

Ниже рассмотрим возможности приложения

### Просмотреть доступные квесты
Url: http://localhost:5001/api/quests/user/{userId} (GET)

Возвращает доступные для пользователя квесты.

Пример ответа:
```
# curl --location 'http://localhost:5001/api/quests/user/a8587ff3-432c-4d91-920e-d1d50c07558e'
# code - 200

[
  {
    "id": "7362029a-4a34-4d8a-9a66-df6fc3a98af5",
    "title": "Повышение статуса жизни2",
    "conditions": [
      {
        "id": 2,
        "type": "Убить монстра",
        "aim": "Пописить в унитаз",
        "amount": 20
      }
    ],
    "reward": {
      "id": 2,
      "experience": 20,
      "items": [],
      "currency": 20
    },
    "requirement": {
      "id": 2,
      "minimumLevel": 1
    }
  },
  {
    "id": "c09f9428-608f-4de5-84b2-dd6592f9a734",
    "title": "Повышение статуса жизни",
    "conditions": [
      {
        "id": 1,
        "type": "Убить монстра",
        "aim": "Покакить в унитаз",
        "amount": 10
      }
    ],
    "reward": {
      "id": 1,
      "experience": 10,
      "items": [],
      "currency": 10
    },
    "requirement": {
      "id": 1,
      "minimumLevel": 0
    }
  },
  {
    "id": "f57190f5-4857-44cd-8d64-dd7b01a87ef4",
    "title": "Повышение статуса жизни3",
    "conditions": [
      {
        "id": 3,
        "type": "Убить монстра",
        "aim": "Пописить и покакить в унитаз",
        "amount": 10
      }
    ],
    "reward": {
      "id": 3,
      "experience": 30,
      "items": [],
      "currency": 30
    },
    "requirement": {
      "id": 3,
      "minimumLevel": 2
    }
  }
]

```

### Принять квест
Принимает квест, доступный пользователю и возвращает созданный квест пользователя

Url - http://localhost:5001/api/quests/accept-quest (POST)

Модель запроса:
```
{   
  "userId": "a8587ff3-432c-4d91-920e-d1d50c07558e",
  "questId": "c09f9428-608f-4de5-84b2-dd6592f9a734"
}
```

Пример положительного ответа:
```
# curl --location 'http://localhost:5001/api/quests/accept-quest' \
--header 'Content-Type: application/json' \
--data '{
  "userId": "a8587ff3-432c-4d91-920e-d1d50c07558e",
  "questId": "c09f9428-608f-4de5-84b2-dd6592f9a734"
}'

# code - 200

{
  "id": "afa416c2-4b05-42e8-aab8-ec0f9b18f1fe",
  "quest": {
    "id": "c09f9428-608f-4de5-84b2-dd6592f9a734",
    "title": "Повышение статуса жизни",
    "conditions": [
      {
        "id": 1,
        "type": "Убить монстра",
        "aim": "Покакить в унитаз",
        "amount": 10
      }
    ],
    "reward": {
      "id": 1,
      "experience": 10,
      "items": [],
      "currency": 10
    },
    "requirement": {
      "id": 1,
      "minimumLevel": 0
    }
  },
  "status": {
    "status": "Accepted"
  },
  "conditionProgresses": [
    {
      "id": 1,
      "condition": {
        "id": 1,
        "type": "Убить монстра",
        "aim": "Покакить в унитаз",
        "amount": 10
      },
      "progress": 0
    }
  ]
}
```

Пример неправильного ответа

```
# curl --location 'http://localhost:5001/api/quests/accept-quest' \
--header 'Content-Type: application/json' \
--data '{
  "userId": "a8587ff3-432c-4d91-920e-d1d50c07558e",
  "questId": "c09f9428-608f-4de5-84b2-dd6592f9a734"
}'

# code - 400

{
    "message": "Невозможно создать квест для пользователя, не подходит по условиям",
    "Errors": {}
}

```

### Обновление прогресса выполнения квестов.
Обновляет прогресс выполения квестов и возвращает выполняемое условие

Url - http://localhost:5001/api/quests/{questId}/user/{userId} (PATCH)

Модель запроса: 
```
conditionId: int,
progress: int
```

Пример правильного ответа:

```
# curl -X 'PATCH' \
  'http://localhost:5001/api/quests/c09f9428-608f-4de5-84b2-dd6592f9a734/user/a8587ff3-432c-4d91-920e-d1d50c07558e' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{
  "conditionId": 1,
  "progress": 10
}'

# code - 200

{
  "id": 1,
  "condition": {
    "id": 1,
    "type": "Убить монстра",
    "aim": "Покакить в унитаз",
    "amount": 10
  },
  "progress": 10
}
```

Пример неправильного ответа:
```
# curl -X 'PATCH' \
  'http://localhost:5001/api/quests/c09f9428-608f-4de5-84b2-dd6592f9a734/user/a8587ff3-432c-4d91-920e-d1d50c07558e' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{
  "conditionId": 1,
  "progress": 10
}'

# code - 400

{
  "message": "Невозможно изменить прогресс. Нарушено условие. Учитывайте, прогресс не может уменьшаться и не превышать условия квеста",
  "Errors": {}
}
```

### Завершение квеста и получение награды
Завершает квест и добавляет пользователю награды, а также возвращает сам пользовательского квеста 

Url - http://localhost:5001/api/quests/finish (POST)

Модель запроса

```
questId: uuid,
userId: uuid
```

Пример правильного ответа:
```
# curl -X 'POST' \
  'http://localhost:5001/api/quests/finish' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{
  "questId": "c09f9428-608f-4de5-84b2-dd6592f9a734",
  "userId": "a8587ff3-432c-4d91-920e-d1d50c07558e"
}'

# code - 200

{
  "id": "afa416c2-4b05-42e8-aab8-ec0f9b18f1fe",
  "quest": {
    "id": "c09f9428-608f-4de5-84b2-dd6592f9a734",
    "title": "Повышение статуса жизни",
    "conditions": [
      {
        "id": 1,
        "type": "Убить монстра",
        "aim": "Покакить в унитаз",
        "amount": 10
      }
    ],
    "reward": {
      "id": 1,
      "experience": 10,
      "items": [],
      "currency": 10
    },
    "requirement": {
      "id": 1,
      "minimumLevel": 0
    }
  },
  "status": {
    "status": "Finished"
  },
  "conditionProgresses": [
    {
      "id": 1,
      "condition": {
        "id": 1,
        "type": "Убить монстра",
        "aim": "Покакить в унитаз",
        "amount": 10
      },
      "progress": 10
    }
  ]
}
```

Пример неправильного ответа:
```
# curl -X 'PATCH' \
  'http://localhost:5001/api/quests/c09f9428-608f-4de5-84b2-dd6592f9a734/user/a8587ff3-432c-4d91-920e-d1d50c07558e' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{
  "conditionId": 1,
  "progress": 10
}'

# code - 400

{
  "Message": "Невозможно завершить квест. Не выполнены условия (this.Progresses для получения условий)",
  "Errors": {
    "Uncompleted Conditions": [
      {
        "Condition": {
          "Type": "Убить монстра",
          "Aim": "Покакить в унитаз",
          "Amount": 10,
          "Id": 1,
          "DomainEvents": []
        },
        "Progress": 5,
        "Id": 1,
        "DomainEvents": []
      }
    ]
  }
}
```