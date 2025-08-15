using Domain;
using Utilities;
using Enums.Marketplace;

namespace Persistence.Tools;

public static class DocumentTools
{
	public static DocumentBox DocumentBoxGenerator(
		DocumentType documentType,
		List<AccountCoding> accountCodings, UserAssets userAssets)
	{
		DocumentBox result =
			DocumentBox.Create(documentType, accountCodings, userAssets);

		return result;
	}
}

public class DocumentBox : object
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	private DocumentBox() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	{
	}

	public string DocumentFor { get; private set; }

	public string NotificationText { get; private set; }

	public static DocumentBox Create(
		DocumentType documentType, List<AccountCoding> accountCodings, UserAssets userAssets)
	{
		DocumentBox result =
			new DocumentBox();

		var dateTime = DateTime.Now;
		var date = dateTime.ToShamsi(stateDate: 2);
		var time = dateTime.ToString(format: "HH:mm");

		var balanceGold = userAssets.AssetsGold;
		var balanceWallet = userAssets.AssetsWallet;

		switch (documentType)
		{
			//  شارژ کیف پول
			case DocumentType.Deposit:
			{
				var debtorAmount =
					accountCodings
						.Where(x => x.Code.StartsWith(AccountCoding.UserMoneyAssetsCode))
						.Where(x => x.IsDebtor == true)
						.Where(x => x.UseParentDocument == true)
						.First()
						.Amount;
				
				result.DocumentFor =
					string.Format(Resources.Messages.DocumentForTextDocumentTypeDeposit
						, userAssets.Profile!.FullName, debtorAmount, date, time);

				var balanceWalletRound = Math.Round(balanceWallet);
				
				result.NotificationText =
					string.Format(Resources.Messages.NotificationTextDocumentTypeDeposit,
						debtorAmount, balanceWalletRound, time, date);
				
				break;
			}
			// برداشت از کیف پول
			case DocumentType.Withdraw:
			{
				var debtorAmount =
					accountCodings
						.Where(x => x.Code.StartsWith(AccountCoding.UserBankAccountCode))
						.Where(x => x.IsDebtor == true)
						.First()
						.Amount;
				
				result.DocumentFor =
					string.Format(Resources.Messages.DocumentForTextDocumentTypeWithdraw,
						userAssets.Profile!.FullName, debtorAmount, date, time);

				result.NotificationText =
					string.Format(Resources.Messages.NotificationTextDocumentTypeWithdraw,
						debtorAmount, balanceWallet, time, date);

				break;
			}
			//  خرید طلای آب شده
			case DocumentType.GoldPurchase:
			{
				var debtorGold =
					accountCodings
						.Where(x => x.Code.StartsWith(AccountCoding.UserGoldAssetsCode))
						.Where(x => x.IsDebtor == true)
						.First()
						.GoldSoot;
				
				result.DocumentFor =
					string.Format(Resources.Messages.DocumentForTextDocumentTypeBuyNow,
						userAssets.Profile!.FullName, debtorGold, date, time);

				result.NotificationText =
					string.Format(Resources.Messages.NotificationTextDocumentTypeBuyNow,
						debtorGold, balanceGold, time, date);

				break;
			}
			//  فروش طلای آب شده
			case DocumentType.SaleOfGoldCode:
			{
				var debtor =
					accountCodings
						.Where(x => x.Code.StartsWith(AccountCoding.UserGoldAssetsCode))
						.Where(x => x.IsDebtor == false)
						.First()
						.GoldSoot;
				
				result.DocumentFor =
					string.Format(Resources.Messages.DocumentForTextDocumentTypeSellNow,
						userAssets.Profile!.FullName, debtor, date, time);

				result.NotificationText =
					string.Format(Resources.Messages.NotificationTextDocumentTypeSellNow,
						debtor, balanceGold, time, date);

				break;
			}
			// سند هزینه رفرال
			case DocumentType.Referal:
			{
				var debtorGold =
					accountCodings
						.Where(x => x.Code.StartsWith(AccountCoding.UserGoldAssetsCode))
						.Where(x => x.IsDebtor == true)
						.First()
						.GoldSoot;
				
				result.DocumentFor =
					string.Format(Resources.Messages.DocumentForTextDocumentTypeReferal,
						userAssets.Profile!.FullName, debtorGold, date, time);

				result.NotificationText =
					string.Format(Resources.Messages.NotificationTextDocumentTypeReferal,
						debtorGold, balanceGold, time, date);

				break;
			}
			default:
			{
				throw new ArgumentOutOfRangeException(nameof(documentType), documentType, null);
			}
		}

		return result;
	}
}