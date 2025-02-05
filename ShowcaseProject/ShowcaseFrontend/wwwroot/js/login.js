//document.getElementById("loginForm").addEventListener("submit", async function (event) {
//    event.preventDefault();

//    const username = document.getElementById("username").value;
//    const password = document.getElementById("password").value;

//    const response = await fetch("http://localhost:5001/login", {
//        method: "POST",
//        headers: { "Content-Type": "application/json" },
//        body: JSON.stringify({ username, password })
//    });

//    const message = document.getElementById("loginMessage");

//    if (response.ok) {
//        const data = await response.json();
//        localStorage.setItem("jwtToken", data.access_token);
//        message.textContent = "Login succesvol!";

//        // Doorsturen naar de homepagina of dashboard
//        window.location.href = "/";
//    } else {
//        message.textContent = "Login mislukt. Controleer je gegevens.";
//    }
//});
