<template>
  <el-table-v2
      :columns="CreateColumns"
      :data="CreateData"
      :width="viewportWidth"
      :height="viewportHeight*0.7"
      fixed
      :gutter = "1"
      :row-height = "30 "
      :sort-by = "sortState"
      @column-sort = "OnSort"
      :row-event-handlers="OpenPage"
  >     
    <template #empty>
      <div class="flex items-center justify-center h-100%">
        <el-empty 
          description="Нет данных(проверьте подключение к сети)"
        />
      </div>
    </template>
  </el-table-v2>
  <div class = "pageButtons">
    <el-button v-for = "n in props.countPage" circle @click = "PageSet(n)">{{n}}</el-button>
  </div>
  <div><el-button @click = "Create">Create new</el-button></div>
</template>
<style scoped>
  .pageButtons {
    display: flex;
    justify-content: center;
    flex-wrap: wrap;
  }
</style>  
<script lang="ts" setup>
import type {SortType, TableColumn, TableData} from '@/types/TableTypes.ts';
import {computed, onMounted, reactive, ref} from "vue";
import router from '@/router/index.ts';
import {useRoute} from 'vue-router';
import type {SortBy} from 'element-plus'
import {SortOrder} from "element-plus/es/components/table-v2/src/constants";
import type { RowEventHandlers  } from 'element-plus'

  const viewportWidth = window.innerWidth;
  const viewportHeight = window.innerHeight;
  const route = useRoute();
  const sortState = ref<SortBy>({
    key: 'column-0',
    order: SortOrder.ASC,
  })
  const props = defineProps <{
        columns: TableColumn[]
        data: TableData[]
        apiBase: string,
        countPage: number;
      }> ();
  const CreateColumns = computed(() => {
    return props.columns.map((column) => {
    return {
      ...column,
      width: column.width ?? 0.1 * viewportWidth,
      sortable: column.sortable ?? true
    }});
  })
  const CreateData = computed(() =>
  {
    if (!props.data || !props.columns) {
      return []
    }
    return props.data.map((datarow, keyForData) => {
      const row: TableData  = {
        id: `${datarow.id || keyForData}`,
      }
      props.columns.forEach(((column, index) => {
        if(datarow[column.dataKey] !== undefined) {
          row[column.dataKey] = datarow[column.dataKey];
        }
        else {
          const keys = Object.keys(datarow);
          if (keys.length > 0) {
            const dataIndex = index < keys.length ? index : 0;
            const key = keys[dataIndex];
            if(key && datarow[key] !== undefined) {
              row[column.dataKey] = datarow[key]
            }
          } else {  
            row[column.dataKey] = '';
          }
        }
      }))
      return row;
    })
  });
  
  const PageSet = ((n : number) =>
  {
    router.push({query: {...route.query, NumberPage: n}});
  })
  const OnSort = (sortBy: SortBy) =>
  {
    sortState.value = sortBy;
    router.push({query: {...route.query, sortKey: sortState.value.key.toString(), sortType: sortState.value.order.toString()}});
  }
  const OpenPage : RowEventHandlers = {
    onClick: (row: any) =>
    {
      let id = row.rowData.id;
      let x = props.data[id];
      if(x !== undefined) {
        id = x.studentId;
      }
      debugger;
      router.push(`${props.apiBase}/${id}`);
    }
  }
const Create = () => {
  router.push(`${props.apiBase}/${undefined}`);
}
</script>   