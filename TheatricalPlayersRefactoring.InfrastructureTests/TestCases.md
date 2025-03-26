# Cenários de Teste - Infrastructure Layer

| Nome do Teste | Cenário | Método | Input | Output | Implementado |
|---------------|----------|---------|--------|---------|--------------|
| InvoiceRepository_WhenGettingExistingInvoice_ThenReturnsInvoice | Validar recuperação de fatura existente | InvoiceRepository.GetByIdAsync | Id válido | Invoice | Y |
| InvoiceRepository_WhenInvoiceNotFound_ThenThrowsKeyNotFoundException | Validar tratamento de fatura não encontrada | InvoiceRepository.GetByIdAsync | Id inválido | KeyNotFoundException | Y |
| InvoiceRepository_WhenSavingNewInvoice_ThenPersistsToDatabase | Validar persistência de nova fatura | InvoiceRepository.SaveAsync | Invoice válida | Void | Y |
| InvoiceRepository_WhenUpdatePath_ThenPersistsToDatabase | Validar atualização de fatura existente | InvoiceRepository.UpdateAsync | Invoice modificada | Void | Y |