<template>
    <div class="flex min-h-screen">
      <aside class="w-64 bg-gray-800 text-white p-4">
        <h1 class="text-xl font-bold mb-4">Admin Rentas</h1>
        <ul class="space-y-2">
          <li v-for="item in items" :key="item.path">
            <router-link :to="item.path" class="block py-1 hover:text-blue-300">{{ item.label }}</router-link>
          </li>
        </ul>
      </aside>
      <main class="flex-1 bg-gray-100 p-6">
        <router-view />
      </main>
    </div>
  </template>
  
  <script>
  export default {
    setup() {
      const items = [
        { label: 'Contribuyentes', path: '/admin/contribuyentes' },
        { label: 'Inmuebles', path: '/admin/inmuebles' },
        { label: 'Tributos', path: '/admin/tributos' },
        { label: 'Pagos', path: '/admin/pagos' },
        { label: 'Notificaciones', path: '/admin/notificaciones' },
        { label: 'Usuarios', path: '/admin/usuarios' },
        { label: 'Municipios', path: '/admin/municipios' },
        { label: 'Estados', path: '/admin/estados' },
        { label: 'Estado Tributo', path: '/admin/estadostributo' },
        { label: 'Localidades', path: '/admin/localidades' },
        { label: 'Tipo Documento', path: '/admin/tiposdocumento' },
        { label: 'Periodicidades', path: '/admin/periodicidades' },
        { label: 'Tipo Impuesto', path: '/admin/tiposimpuesto' },
        { label: 'Valor Impuesto', path: '/admin/valorestipoimpuesto' },
        { label: 'Tipo Inmueble', path: '/admin/tiposinmueble' },
        { label: 'Titulares Inmueble', path: '/admin/titularidadinmueble' },
        { label: 'Valuaciones Inmueble', path: '/admin/valuacionesinmueble' },
        { label: 'Obras Públicas', path: '/admin/obraspublicas' },
        { label: 'Inmuebles en Obras', path: '/admin/inmueblesobras' },
        { label: 'Medios de Pago', path: '/admin/mediospago' },
        { label: 'Planes de Pago', path: '/admin/planespago' },
        { label: 'Cuotas Plan', path: '/admin/cuotasplan' },
        { label: 'Intereses Mora', path: '/admin/interesmora' },
        { label: 'Plantillas Notificación', path: '/admin/plantillasnotificacion' },
        { label: 'Cola Notificación', path: '/admin/colanotificacion' },
        { label: 'Auditoría', path: '/admin/auditoria' }
      ];
  
      return { items };
    }
  };
  </script>
  
  <style scoped>
  .router-link-active {
    font-weight: bold;
    color: #3b82f6;
  }
  </style>