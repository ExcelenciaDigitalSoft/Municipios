<template>
  <div class="p-4">
    <div class="mb-4">
      <h2 class="text-2xl font-bold">Contribuyentes</h2>
      <form @submit.prevent="crearContribuyente" class="grid grid-cols-2 gap-4 mt-4">
        <input v-model="nuevo.nombres" placeholder="Nombres" class="input" />
        <input v-model="nuevo.apellidos" placeholder="Apellidos" class="input" />
        <input v-model="nuevo.numeroDocumento" placeholder="Documento" class="input" />
        <input v-model="nuevo.email" placeholder="Email" class="input" />
        <input v-model="nuevo.calle" placeholder="Calle" class="input" />
        <input v-model="nuevo.numero" placeholder="Número" class="input" />
        <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
      </form>
    </div>

    <div class="card mt-4">
      <ul>
        <li v-for="c in contribuyentes" :key="c.id" class="py-2 border-b flex justify-between items-center">
          <span>{{ c.nombres }} {{ c.apellidos }} - {{ c.numeroDocumento }}</span>
          <div class="space-x-2">
            <button class="btn" @click="seleccionarEditar(c)">Editar</button>
            <button class="btn bg-red-500" @click="eliminarContribuyente(c.id)">Eliminar</button>
          </div>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import { ref, onMounted } from 'vue';
import axios from 'axios';

export default {
  setup() {
    const contribuyentes = ref([]);
    const editar = ref(false);
    const nuevo = ref({
      id: null,
      nombres: '',
      apellidos: '',
      numeroDocumento: '',
      email: '',
      calle: '',
      numero: '',
      estadoId: 1,
      idMunicipio: 1,
      idLocalidad: 1,
      idTipoDocumento: 1
    });

    const token = localStorage.getItem('token');
    const headers = { Authorization: `Bearer ${token}` };

    const cargarContribuyentes = async () => {
      const res = await axios.get('/api/admin/gestion/contribuyentes', { headers });
      contribuyentes.value = res.data;
    };

    const crearContribuyente = async () => {
      if (editar.value) {
        await axios.put(`/api/contribuyentes/${nuevo.value.id}`, nuevo.value, { headers });
      } else {
        await axios.post('/api/admin/gestion/contribuyentes', nuevo.value, { headers });
      }
      await cargarContribuyentes();
      limpiarFormulario();
    };

    const eliminarContribuyente = async (id) => {
      await axios.delete(`/api/admin/gestion/contribuyentes/${id}`, { headers });
      await cargarContribuyentes();
    };

    const seleccionarEditar = (c) => {
      Object.assign(nuevo.value, { ...c });
      editar.value = true;
    };

    const limpiarFormulario = () => {
      Object.assign(nuevo.value, {
        id: null,
        nombres: '',
        apellidos: '',
        numeroDocumento: '',
        email: '',
        calle: '',
        numero: '',
        estadoId: 1,
        idMunicipio: 1,
        idLocalidad: 1,
        idTipoDocumento: 1
      });
      editar.value = false;
    };

    onMounted(() => cargarContribuyentes());

    return {
      contribuyentes,
      nuevo,
      editar,
      crearContribuyente,
      eliminarContribuyente,
      seleccionarEditar
    };
  }
};
</script>

<style scoped>
.input {
  padding: 0.5rem;
  border: 1px solid #ccc;
  border-radius: 0.5rem;
}
.btn {
  background: #2d6cdf;
  color: white;
  padding: 0.5rem;
  border-radius: 0.5rem;
  font-weight: bold;
}
.card {
  background: white;
  padding: 1rem;
  border-radius: 1rem;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}
</style>
