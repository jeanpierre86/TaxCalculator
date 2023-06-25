document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("calculateBtn").addEventListener("click", function () {
        var annualIncome = document.getElementById("AnnualIncome").value;
        var postalCode = document.getElementById("PostalCode").value;
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
            if (data.Errors && data.Errors.length > 0) {
                resultContainer.innerText = "Error: " + data.Errors.join("; ");
            }
            else {
                resultContainer.innerText = "Income Tax: " + data.IncomeTax;
            }            
        })
        .catch(error => {
            resultContainer.innerText = error;
        });
    });
});