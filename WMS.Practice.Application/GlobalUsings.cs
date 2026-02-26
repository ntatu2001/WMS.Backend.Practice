global using System.Runtime.Serialization;
global using System.Text.Json.Serialization;
global using AutoMapper;
global using MediatR;
global using WMS.Practice.Application.Commands.EmployeeCommands.Employees;
global using WMS.Practice.Application.Commands.InventoryReceiptCommands.InventoryReceiptEntries;
global using WMS.Practice.Application.Commands.MaterialCommands.MaterialLots;
global using WMS.Practice.Application.Commands.MaterialCommands.MaterialSubLots;
global using WMS.Practice.Application.DTOs.PersonDTOs.Customers;
global using WMS.Practice.Application.DTOs.PersonDTOs.Employees;
global using WMS.Practice.Application.DTOs.PersonDTOs.Suppliers;
global using WMS.Practice.Application.Exceptions;
global using WMS.Practice.Application.Extensions;
global using WMS.Practice.Application.Services.InventoryLogs.InventoryIssues;
global using WMS.Practice.Application.Services.InventoryLogs.InventoryReceipts;
global using WMS.Practice.Domain.AggregateModels.InventoryIssueAggregate;
global using WMS.Practice.Domain.AggregateModels.InventoryLogAggregate;
global using WMS.Practice.Domain.AggregateModels.InventoryReceiptAggregate;
global using WMS.Practice.Domain.AggregateModels.MaterialAggregate;
global using WMS.Practice.Domain.AggregateModels.PersonAggregate;
global using WMS.Practice.Domain.AggregateModels.StockTakeAggregate;
global using WMS.Practice.Domain.AggregateModels.StorageAggregate;
global using WMS.Practice.Domain.DomainEvents.InventoryLogEvents;
global using WMS.Practice.Domain.Enums;
global using WMS.Practice.Domain.InterfaceRepositories.IInventoryIssue;
global using WMS.Practice.Domain.InterfaceRepositories.IInventoryLog;
global using WMS.Practice.Domain.InterfaceRepositories.IInventoryReceipt;
global using WMS.Practice.Domain.InterfaceRepositories.IMaterial;
global using WMS.Practice.Domain.InterfaceRepositories.IPerson;
global using WMS.Practice.Domain.InterfaceRepositories.IStockTake;
global using WMS.Practice.Domain.InterfaceRepositories.IStorage;
global using WMS.Practice.Application.DTOs.MaterialDTOs.Materials;
global using WMS.Practice.Application.DTOs.MaterialDTOs.MaterialSubLots;
global using WMS.Practice.Application.DTOs.StorageDTOs.Locations;
global using WMS.Practice.Application.DTOs.MaterialDTOs.MaterialLots;





