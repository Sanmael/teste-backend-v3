# Cenários de Teste - Application Layer

| Nome do Teste | Cenário | Método | Input | Output | Implementado |
|---------------|----------|---------|--------|---------|--------------|
| GenerateBillCommand_WhenCustomerNameIsEmpty_ThenThrowsValidationException | Validar que nome do cliente não pode ser vazio | ValidateGenerateBillCommandHandler.Handle | Command com CustomerName vazio | ValidationException | Y |
| GenerateBillCommand_WhenCustomerNameExceeds100Chars_ThenThrowsValidationException | Validar limite máximo de caracteres do nome | ValidateGenerateBillCommandHandler.Handle | Command com CustomerName > 100 chars | ValidationException | Y |
| GenerateBillCommand_WhenPerformanceListIsEmpty_ThenThrowsValidationException | Validar que lista de performances não pode ser vazia | ValidateGenerateBillCommandHandler.Handle | Command sem performances | ValidationException | Y |
| GenerateBillCommand_WhenInputIsValid_ThenSavesInvoiceAndPublishesMessage | Validar fluxo completo de geração de fatura | GenerateBillCommandHandler.Handle | Command válido | GenerateBillResult com Id | Y |
| GenerateBillCommand_WhenDatabaseIsUnavailable_ThenThrowsException | Validar tratamento de erro do banco | GenerateBillCommandHandler.Handle | Command válido + DB offline | Exception | Y |
| GetBillQuery_WhenInvoiceExists_ThenReturnsBillDto | Validar recuperação de fatura existente | GetBillQueryHandler.Handle | InvoiceId válido | BillDto | Y |
| GetBillQuery_WhenInvoiceNotFound_ThenThrowsNotFoundException | Validar tratamento de fatura não encontrada | GetBillQueryHandler.Handle | InvoiceId inválido | NotFoundException | Y |
| GenerateStatementTxt_WhenValuesIsCorrect_ShouldReturnExpected | Validar formatação do extrato em texto | TextStatementFactory.Generate | Invoice válida | String formatada | Y |
| GenerateStatementXmlNew_WhenValuesIsCorrect_ShouldReturnExpected | Validar geração de XML válido | XmlStatementFactory.Generate | Invoice válida | XML válido | Y |
| GenerateBillCommand_WhenPlayTypeIsInvalid_ThenThrowsValidationException | Validar tipos de peça permitidos | ValidateGenerateBillCommandHandler.Handle | Command com tipo inválido | ValidationException | Y |
| GenerateBillCommand_WhenAudienceIsNegative_ThenThrowsValidationException | Validar público não negativo | ValidateGenerateBillCommandHandler.Handle | Command com público negativo | ValidationException | Y |
| GenerateBillCommand_WhenQueueIsUnavailable_ThenThrowsException | Validar tratamento de erro da fila | GenerateBillCommandHandler.Handle | Command válido + Queue offline | Exception | Y |