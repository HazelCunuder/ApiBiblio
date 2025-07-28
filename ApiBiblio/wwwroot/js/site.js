// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// wwwroot/js/site.js

// Gestion de l'authentification
function getAuthToken() {
    return localStorage.getItem('token');
}

function getUserRole() {
    return localStorage.getItem('userRole');
}

function logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('userRole');
    window.location.href = '/Home/Login';
}

// Intercepter les requêtes pour ajouter le token
async function apiRequest(url, options = {}) {
    const token = getAuthToken();

    const defaultOptions = {
        headers: {
            'Content-Type': 'application/json',
            ...(token && { 'Authorization': `Bearer ${token}` })
        }
    };

    const mergedOptions = {
        ...defaultOptions,
        ...options,
        headers: {
            ...defaultOptions.headers,
            ...options.headers
        }
    };

    try {
        const response = await fetch(url, mergedOptions);

        if (response.status === 401) {
            logout();
            return;
        }

        return response;
    } catch (error) {
        console.error('Erreur API:', error);
        throw error;
    }
}

// Vérifier l'authentification au chargement de la page
document.addEventListener('DOMContentLoaded', () => {
    const token = getAuthToken();
    const currentPath = window.location.pathname;

    // Pages qui nécessitent une authentification
    const protectedPaths = ['/Home/Dashboard', '/Livre', '/Membre', '/Emprunt'];
    const isProtectedPath = protectedPaths.some(path => currentPath.startsWith(path));

    if (isProtectedPath && !token) {
        window.location.href = '/Home/Login';
    } else if (currentPath === '/Home/Login' && token) {
        window.location.href = '/Home/Dashboard';
    }
});

// Utilitaires pour les formulaires
function showAlert(message, type = 'info') {
    const alertDiv = document.createElement('div');
    alertDiv.className = `alert alert-${type} alert-dismissible fade show`;
    alertDiv.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    `;

    const container = document.querySelector('.container');
    container.insertBefore(alertDiv, container.firstChild);

    setTimeout(() => {
        alertDiv.remove();
    }, 5000);
}

function formatDate(dateString) {
    return new Date(dateString).toLocaleDateString('fr-FR');
}

function formatDateTime(dateString) {
    return new Date(dateString).toLocaleString('fr-FR');
}