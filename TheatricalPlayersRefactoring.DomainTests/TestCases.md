# Cenários de Teste - Domain Layer

## Entities

### Invoice Tests
| Nome do Teste | Cenário | Método | Input | Output | Implementado |
|---------------|----------|---------|--------|---------|--------------|
| Invoice_WhenCreated_ThenHasNewId | Validar criação de fatura | Constructor | CustomerName | Invoice com Id | Y |
| Invoice_WhenAddingPerformance_ThenUpdatesTotalCredits | Validar atualização de créditos | AddPerformance | Performance | Credits atualizados | Y |
| Invoice_WhenCalculatingTotalAmount_ThenSumsAllPerformances | Validar cálculo do valor total | CalculateTotalAmount | N/A | Money total | Y |
| Invoice_WhenSavingExtract_ThenUpdatesPath | Validar salvamento do extrato | SaveExtract | string path | ExtractPath atualizado | Y |

### Performance Tests
| Nome do Teste | Cenário | Método | Input | Output | Implementado |
|---------------|----------|---------|--------|---------|--------------|
| Performance_WhenCreated_ThenHasValidId | Validar criação de performance | Create | Play, Audience | Performance válida | Y |
| Performance_WhenCalculatingAmount_ThenUsesPlayStrategy | Validar cálculo do valor | CalculateAmount | N/A | Money calculado | Y |
| Performance_WhenCalculatingCredits_ThenUsesPlayStrategy | Validar cálculo de créditos | CalculateCredits | N/A | Credits calculados | Y |

### Play Tests
| Nome do Teste | Cenário | Método | Input | Output | Implementado |
|---------------|----------|---------|--------|---------|--------------|
| Play_WhenCreated_ThenHasValidProperties | Validar criação de peça | Constructor | name, lines, type | Play válida | Y |
| Play_WhenCalculatingAmount_ThenUsesCorrectStrategy | Validar estratégia de cálculo | CalculateAmount | Audience | Money calculado | Y |
| Play_WhenInvalidPlayType_ThenThrowsException | Validar tipo de peça inválido | SetBillingStrategy | PlayType inválido | ArgumentException | Y |

## Value Objects

### Money Tests
| Nome do Teste | Cenário | Método | Input | Output | Implementado |
|---------------|----------|---------|--------|---------|--------------|
| Money_WhenAdding_ThenReturnsSumValue | Validar soma de valores | Add | Money, Money | Money somado | Y |
| Money_WhenCreatingZero_ThenReturnsZeroValue | Validar criação de zero | Zero | N/A | Money(0) | Y |

### Lines Tests
| Nome do Teste | Cenário | Método | Input | Output | Implementado |
|---------------|----------|---------|--------|---------|--------------|
| Lines_WhenBelowMinimum_ThenClampsToMin | Validar limite mínimo | Constructor | < 1000 | 1000 | Y |
| Lines_WhenAboveMaximum_ThenClampsToMax | Validar limite máximo | Constructor | > 4000 | 4000 | Y |
| Lines_WhenCalculatingBaseAmount_ThenDividesByTen | Validar cálculo base | GetBaseAmount | N/A | Value/10 | Y |

### CustomerName Tests
| Nome do Teste | Cenário | Método | Input | Output | Implementado |
|---------------|----------|---------|--------|---------|--------------|
| CustomerName_WhenEmpty_ThenThrowsException | Validar nome vazio | Constructor | "" | ArgumentException | Y |
| CustomerName_WhenValid_ThenSetsValue | Validar nome válido | Constructor | "Nome" | CustomerName | Y |

### Credits Tests
| Nome do Teste | Cenário | Método | Input | Output | Implementado |
|---------------|----------|---------|--------|---------|--------------|
| Credits_WhenAdding_ThenReturnsSumValue | Validar soma de créditos | Add | Credits, Credits | Credits somados | Y |
| Credits_WhenCreatingZero_ThenReturnsZeroValue | Validar criação zero | Zero | N/A | Credits(0) | Y |

### Audience Tests
| Nome do Teste | Cenário | Método | Input | Output | Implementado |
|---------------|----------|---------|--------|---------|--------------|
| Audience_WhenNegative_ThenThrowsException | Validar público negativo | Constructor | -1 | ArgumentException | Y |
| Audience_WhenValid_ThenSetsValue | Validar público válido | Constructor | 100 | Audience | Y |

## Billing Strategies

### Comedy Strategy Tests
| Nome do Teste | Cenário | Método | Input | Output | Implementado |
|---------------|----------|---------|--------|---------|--------------|
| ComedyStrategy_WhenCalculatingAmount_ThenAppliesFormula | Validar cálculo de valor | CalculateAmount | Lines, Audience | Money calculado | Y |
| ComedyStrategy_WhenCalculatingCredits_ThenIncludesBonus | Validar cálculo de créditos | CalculateCredits | Audience | Credits com bônus | Y |

### Tragedy Strategy Tests
| Nome do Teste | Cenário | Método | Input | Output | Implementado |
|---------------|----------|---------|--------|---------|--------------|
| TragedyStrategy_WhenCalculatingAmount_ThenAppliesFormula | Validar cálculo de valor | CalculateAmount | Lines, Audience | Money calculado | Y |
| TragedyStrategy_WhenCalculatingCredits_ThenChecksThreshold | Validar cálculo de créditos | CalculateCredits | Audience | Credits | Y |

### History Strategy Tests
| Nome do Teste | Cenário | Método | Input | Output | Implementado |
|---------------|----------|---------|--------|---------|--------------|
| HistoryStrategy_WhenCalculatingAmount_ThenCombinesStrategies | Validar cálculo combinado | CalculateAmount | Lines, Audience | Money combinado | Y |
| HistoryStrategy_WhenCalculatingCredits_ThenCombinesCredits | Validar créditos combinados | CalculateCredits | Audience | Credits combinados | Y |
