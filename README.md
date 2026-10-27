# VirtualAssistant

Assistente virtual com **RAG** (Retrieval-Augmented Generation) em **.NET 8**. Ele lê um manual em PDF, guarda os trechos como vetores e responde perguntas por **texto ou áudio**, usando só o contexto que o perfil do usuário pode ver.

## Como funciona

```
PDF ──► ingestão em trechos ──► embeddings (Ollama) ──► Qdrant
                                                          │
pergunta (texto/áudio) ──► JWT + perfil ──► busca vetorial ┘
        │                                   │
        └──► contexto da conversa (Redis) ──┴──► prompt ──► LLM (Ollama) ──► resposta
```

- **Ingestão:** o `DocumentIngestionService` divide o PDF em trechos e gera embeddings pelo Ollama.
- **Busca:** o `QdrantService` recupera os trechos mais próximos da pergunta.
- **Permissão:** o `PermissionContextFilter` remove do contexto o que o perfil do usuário não pode ver.
- **Memória:** o `RedisConversationContextService` guarda o histórico da conversa com política de expiração.
- **Áudio:** perguntas por voz passam pelo `HttpAudioService` antes de seguir o mesmo fluxo.

## Arquitetura

Clean Architecture em quatro projetos:

| Projeto | Responsabilidade |
|---|---|
| `VirtualAssistant.Domain` | Entidades: `ChatMessage`, `DocumentChunk`, `UserProfile` |
| `VirtualAssistant.Application` | Casos de uso, montagem de prompt e filtros de contexto |
| `VirtualAssistant.Infrastructure` | Qdrant, Ollama, Redis, JWT e serviço de áudio |
| `VirtualAssistant.API` | ASP.NET Core: endpoints de chat por texto e por áudio |

## Stack

C# · .NET 8 · ASP.NET Core · Qdrant · Ollama · Redis · JWT · Swagger

## Rodando localmente

Pré-requisitos: .NET 8 SDK, Qdrant, Redis e Ollama rodando localmente.

1. Configure em `appsettings.json`:
   ```json
   {
     "Document": { "Path": "caminho/do/manual.pdf" },
     "Ollama": { "Url": "http://localhost:11434" },
     "Qdrant": { "Url": "http://localhost:6334" },
     "ConnectionStrings": { "Redis": "localhost:6379" }
   }
   ```
2. Rode a API:
   ```bash
   dotnet run --project src/VirtualAssistant.API
   ```
3. Abra o Swagger e envie uma pergunta para `POST /api/chat/text` ou `POST /api/chat/audio`.
