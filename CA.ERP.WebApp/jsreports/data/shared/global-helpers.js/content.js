function formatMoney(money){
    var formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'PHP',
        currencyDisplay: 'symbol',
    });
    return formatter.format(money).replace("PHP", "₱");;
}

function formatDateShort(strDate)
{
    var date = new Date(strDate);
    return date.toLocaleDateString();
}