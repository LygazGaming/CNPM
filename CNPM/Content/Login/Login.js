document.addEventListener('DOMContentLoaded', function () {
    const loginButton = document.getElementById('loginButton');
    loginButton.addEventListener('click', function (event) {
        event.preventDefault(); // Prevent default action of anchor tag (navigation)

        const email = document.getElementById('email').value;
        const password = document.getElementById('password').value;
        const emailError = document.getElementById('emailError');
        const passwordError = document.getElementById('passwordError');

        // Clear previous error messages
        emailError.textContent = '';
        passwordError.textContent = '';

        if (email === '') {
            emailError.textContent = 'Email is required.';
            return; // Stop function execution
        }

        if (!email.includes('@')) {
            emailError.textContent = 'Email must contain @.';
            return; // Stop function execution
        }

        if (password === '') {
            passwordError.textContent = 'Password is required.';
            return; // Stop function execution
        }

        // If all validations pass, redirect to the dashboard page
        window.location.href = 'https://localhost:44331/Home/Home'; // Relative path to dashboard.html in "khascc" folder
    });
});
