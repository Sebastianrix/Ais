import { useState, useEffect, useRef} from 'react';
import '../css/SidePanel.css';


const sections = [
    {
        id: 'First',
        title: 'First',
        subsections: [
            { id: 'firstSub', title: 'firstSub'},
            { id: 'firstSub2', title: 'firstSub2' },
        ],
    },
];

export default function SidePanel() {
    const [collapsed, setCollapsed] = useState(false);
    const [activeId, setActiveId] = useState('First');
    const [expandedSections, setExpandedSections] = useState(
      new Set(['First'])
    );
}