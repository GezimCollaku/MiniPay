const BASE_URL = "http://localhost:5214/api";

async function loadProviders() {
    try {
        const response = await fetch(`${BASE_URL}/paymentprovider`);
        if (!response.ok) {
            throw new Error("Gabim gjatë marrjes së të dhënave");
        }
        const providers = await response.json();
        const tableBody =
            document.getElementById("providers-table-body");
        tableBody.innerHTML = "";

        const simSelect =
            document.getElementById("sim-provider");
        simSelect.innerHTML =
            '<option value="">-- Zgjidh Ofrues Aktiv --</option>';
        if (providers.length === 0) {
            tableBody.innerHTML = `
                <tr>
                    <td colspan="5">
                        Nuk ka ofrues të regjistruar.
                    </td>
                </tr>
            `;
            return;
        }

        providers.forEach(provider => {
           const row = document.createElement("tr");
            row.innerHTML = `
                <td>#${provider.id}</td>
                <td>${provider.name}</td>
                <td>${provider.currency}</td>
                <td>
                    ${provider.isActive ? "Aktiv" : "Jo Aktiv"}
                </td>
                <td>
                    <button onclick="toggleStatus(${provider.id}, ${provider.isActive})">
                        Status
                    </button>
                </td>
            `;

            tableBody.appendChild(row);
            if (provider.isActive) {
                const option =
                    document.createElement("option");
                option.value = provider.id;
                option.textContent =
                    `${provider.name} (${provider.currency})`;
                simSelect.appendChild(option);
            }
        });

    } catch (error) {

        console.error(error);
        document.getElementById(
            "providers-table-body"
        ).innerHTML = `
            <tr>
                <td colspan="5">
                    Gabim në lidhje me API.
                </td>
            </tr>
        `;
    }
}

document.getElementById("add-provider-form")
.addEventListener("submit", async (e) => {
    e.preventDefault();
    const newProvider = {
        name:
            document.getElementById("provider-name").value,
        currency:
            document.getElementById("provider-currency").value,
        endpointUrl:
            document.getElementById("provider-url").value,
        isActive: true
    };

    try {
        const response =
            await fetch(`${BASE_URL}/paymentprovider`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(newProvider)
            });

        if (!response.ok) {
            throw new Error();
        }

        document
            .getElementById("add-provider-form")
            .reset();
        loadProviders();

    } catch {
        alert("Shtimi dështoi.");
    }
});

async function toggleStatus(id, currentStatus) {
    try {
        const response =
            await fetch(
                `${BASE_URL}/paymentprovider/${id}`
            );
        const provider =
            await response.json();
        provider.isActive = !currentStatus;
        await fetch(
            `${BASE_URL}/paymentprovider/${id}`,
            {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(provider)
            }
        );
        loadProviders();
    } catch {
        alert("Ndryshimi i statusit dështoi.");
    }
}

document.getElementById("simulation-form")
.addEventListener("submit", async (e) => {
    e.preventDefault();
    const providerId =
        document.getElementById("sim-provider").value;
    const displayZone =
        document.getElementById("json-result");
    displayZone.textContent =
        "Duke procesuar transaksionin...";
    const paymentRequest = {
        amount: parseFloat(
            document.getElementById("sim-amount").value
        ),
        currency:
            document.getElementById("sim-currency").value,
        description:
            document.getElementById("sim-desc").value
    };

    try {
        const response =
            await fetch(
                `${BASE_URL}/transaction/simulate/${providerId}`,
                {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(paymentRequest)
                }
            );

        const result =
            await response.json();
        if (!response.ok) {
            displayZone.textContent =
                JSON.stringify(result, null, 2);
            return;
        }

        displayZone.textContent =
            JSON.stringify(result, null, 2);
    } catch {
        displayZone.textContent =
            "Gabim kritik. Nuk u lidh dot me serverin.";
    }
});

window.onload = loadProviders;


