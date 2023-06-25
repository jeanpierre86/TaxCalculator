document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("calculateBtn").addEventListener("click", function () {
        var annualIncome = document.getElementById("annualincome").value;
        var postalCode = document.getElementById("postalcode").value;
        var resultContainer = document.getElementById("result");

        var request = {
            annualIncome: annualIncome,
            postalCode: postalCode
        };

        fetch("/calculatetax", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(request)
        })
        .then(response => response.json())
        .then(data => {
            resultContainer.innerText = "Income Tax: " + data.IncomeTax;
        })
        .catch(error => {
            resultContainer.innerText = error;
        });
    });
});