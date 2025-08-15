using Domain;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class DocumentRepository : Repository<Document>, IDocumentRepository
{
	internal DocumentRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}

	public async Task<List<Document>>
		AddByAccountCodingsAsync(
		List<AccountCoding> accountCodings,
		Document document, CancellationToken cancellationToken = default)
	{
		var documentParent = new Document
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,
			Description = document.Description,
			
			DocumentFor = document.DocumentFor,
			DocumentNumber = GenerateDocumentNumber(),
		};

		var documentChild = new Document
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,
			Description = document.Description,

			DocumentFor = document.DocumentFor,
			DocumentNumber = GenerateDocumentNumber(),
		};

		foreach (var accountCoding in accountCodings.Where(x => x.UseParentDocument == true))
		{
			var docDetail = new DocumentDetail
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,

				DocumentId = documentParent.Id,

				ParentDocument = null,
				ParentDocumentId = null,

				Amount = accountCoding.Amount,
				GoldSoot = accountCoding.GoldSoot,
				GoldPriceInThisTime = accountCoding.PriceInThisTime,

				IsDebtor = accountCoding.IsDebtor,
				IsCreditor = accountCoding.IsCreditor,

				AccountCoding = accountCoding,
				AccountCodingId = accountCoding.Id,
				
				RelationId = accountCoding.RelationId,
				SubSystemLocalId = accountCoding.SubSystemLocalId
			};

			documentParent.DocumentDetails.Add(docDetail);
		}

		foreach (var accountCoding in accountCodings.Where(x => x.UseParentDocument == false))
		{
			var docDetail = new DocumentDetail
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,

				Document = documentChild,
				DocumentId = documentChild.Id,

				ParentDocument = documentParent,
				ParentDocumentId = documentParent.Id,

				Amount = accountCoding.Amount,
				GoldSoot = accountCoding.GoldSoot,
				GoldPriceInThisTime = accountCoding.PriceInThisTime,

				IsDebtor = accountCoding.IsDebtor,
				IsCreditor = accountCoding.IsCreditor,
				
				AccountCoding = accountCoding,
				AccountCodingId = accountCoding.Id,
				
				RelationId = accountCoding.RelationId,
				SubSystemLocalId = accountCoding.SubSystemLocalId
			};

			documentChild.DocumentDetails.Add(docDetail);
		}

		var docs = new List<Document>();
		
		if (documentParent.DocumentDetails.Any() == true)
		{
			await AddAsync(documentParent, cancellationToken);
			docs.Add(documentParent);
		}

		if (documentChild.DocumentDetails.Any() == true)
		{
			await AddAsync(documentChild, cancellationToken);
			docs.Add(documentChild);
		}

		return docs;
	}

	private string GenerateDocumentNumber()
	{
		// first code
		var result = "02101402000000001"; // lastCode + 1;

		return result;
	}
}