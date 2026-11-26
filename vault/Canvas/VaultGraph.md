<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Athanor Vault Graph — Obsidian Canvas</title>
  <link rel="preconnect" href="https://fonts.googleapis.com" crossorigin="anonymous">
  <style>
    :root {
      --bg-paper: #f4e6c6;
      --bg-paper-dark: #2a1f15;
      --ink-primary: #2d3436;
      --ink-secondary: #636e72;
      --accent-red: #e89b00;
      --accent-blue: #0c7ce8;
      --accent-green: #00bc7f;
      --accent-yellow: #fbdd4d;
    }

    @media (prefers-color-scheme: dark) {
      :root[data-theme="dark"] {
        --bg-paper: #1a1612;
        --bg-paper-dark: #1a1612;
        --ink-primary: #dfdbcc;
        --ink-secondary: #a78d70;
      }
    }

    * { box-sizing: border-box; }

    body {
      margin: 0;
      background: var(--bg-paper);
      color: var(--ink-primary);
      font-family: 'Fira Code', 'JetBrains Mono', monospace;
      line-height: 1.6;
      overflow: hidden;
    }

    .hud {
      position: fixed;
      top: 0; left: 0; right: 0; height: 48px;
      background: var(--bg-paper-dark);
      color: var(--ink-primary);
      font-size: 12px;
      display: flex;
      align-items: center;
      padding: 0 12px;
      gap: 24px;
      border-bottom: 2px solid var(--ink-secondary);
      z-index: 100;
    }

    .hud-section {
      display: flex;
      align-items: center;
      gap: 8px;
    }

    .hud-divider {
      height: 1px;
      background: var(--ink-primary);
      width: 12px;
    }

    .hud-title {
      font-weight: bold;
      margin-right: auto;
    }

    canvas#graphCanvas {
      display: block;
      cursor: grab;
    }

    canvas#graphCanvas:active {
      cursor: grabbing;
    }

    #tooltip {
      position: fixed;
      background: var(--bg-paper-dark);
      color: var(--ink-primary);
      padding: 8px 12px;
      border-radius: 4px;
      font-size: 12px;
      pointer-events: none;
      opacity: 0;
      transition: opacity 0.15s;
      z-index: 101;
      max-width: 300px;
    }

    #tooltip strong {
      color: var(--accent-red);
    }

    .legend {
      position: fixed;
      bottom: 10px;
      right: 10px;
      background: var(--bg-paper-dark);
      padding: 12px;
      border-radius: 6px;
      font-size: 11px;
      max-width: 180px;
      z-index: 101;
    }

    .legend-item {
      display: flex;
      gap: 8px;
      margin-bottom: 6px;
      font-size: 10px;
    }

    .dot { width: 8px; height: 8px; border-radius: 50%; }
  </style>
</head>
<body>
  <div class="hud">
    <div class="hud-title">Athanor — Obsidian Vault Graph</div>
    <div class="hud-section">
      <span>Core Rules: <strong>Athanor.Gameplay</strong></span>
      <div class="hud-divider"></div>
      <span>Game Layer: <strong>Athanor.Game</strong></span>
      <div class="hud-divider"></div>
      <span>Content: <strong>content assets</strong></span>
    </div>
    <div class="hud-section" style="margin-left: auto;">
      <button onclick="toggleTheme()" style="background: transparent; border: 1px solid var(--ink-primary); color: var(--ink-primary); cursor: pointer;">↻ Theme</button>
    </div>
  </div>

  <canvas id="graphCanvas"></canvas>

  <div id="tooltip">Tooltip content goes here</div>

  <div class="legend">
    <div class="legend-item"><span class="dot" style="background: var(--accent-red)"></span> Materials (81)</div>
    <div class="legend-item"><span class="dot" style="background: var(--accent-green)"></span> Recipes (87 pairs)</div>
    <div class="legend-item"><span class="dot" style="background: var(--accent-yellow)"></span> Catalysts (26)</div>
    <div class="legend-item"><span class="dot" style="background: var(--accent-blue)"></span> Aspects (16)</div>
    <div class="legend-item"><span class="dot" style="background: #666"></span> Events (19)</div>
    <div class="legend-item"><span class="dot" style="background: #888"></span> Goals (22)</div>
    <div class="legend-item"><span class="dot" style="background: var(--accent-blue)"></span> Core Layer</div>
  </div>

  <script src="https://cdn.jsdelivr.net/npm/d3@7/dist/d3.min.js"></script>
  <script>
    const data = {
      layers: [
        { name: 'core', color: '#0c7ce8', radius: 25, label: ['RunState', 'ContentDb', 'IgniteResolver', 'ChainPlanner', 'Satchel'] },
        { name: 'game', color: '#e89b00', radius: 30, label: ['BenchView', 'HudView', 'LinkView', 'Cutscenes'] },
        { name: 'content', color: '#fbdd4d', radius: 20, label: ['Materials (81)', 'Recipes (87)', 'Catalysts (26)'] },
        { name: 'data', color: '#00bc7f', radius: 25, label: ['Journal', 'Profile', 'Lang files'] }
      ],
      nodes: [
        // Core layer nodes
        { id: 'content.db', type: 'core', label: 'ContentDb' },
        { id: 'run.state', type: 'core', label: 'RunState' },
        { id: 'ignite', type: 'core', label: 'IgniteResolver' },
        { id: 'chain', type: 'core', label: 'ChainPlanner' },
        { id: 'satchel', type: 'core', label: 'Satchel' },
        { id: 'journal', type: 'core', label: 'Journal' },
        { id: 'lang', type: 'core', label: 'Lang' },
        { id: 'auto.player', type: 'core', label: 'AutoPlayer' },
        
        // Game layer nodes
        { id: 'bench', type: 'game', label: 'BenchView' },
        { id: 'hud', type: 'game', label: 'HudView' },
        { id: 'tile.hover', type: 'game', label: 'TileView' },
        { id: 'links', type: 'game', label: 'LinkView (Motes)' },
        { id: 'cutscene.base', type: 'game', label: 'Cutscene Framework' },
        { id: 'cutscene.discovery', type: 'game', label: 'Reaction Cutscene' },
        { id: 'cutscene.simple', type: 'game', label: 'Simple Cutscenes' },
        
        // Data nodes
        { id: 'materials', type: 'data', label: 'Materials (81)' },
        { id: 'recipes', type: 'data', label: 'Recipes (87 pairs)' },
        { id: 'aspects', type: 'data', label: 'Aspects (16)' },
        { id: 'events', type: 'data', label: 'Events (19)' },
        
        // Core dependencies
        { id: 'depths', type: 'core', label: 'Depth Targets' }
      ],
      links: [
        // Core -> Game flows
        { source: 'content.db', target: 'bench', label: 'material selection' },
        { source: 'ignite', target: 'bench', label: 'react first' },
        { source: 'run.state', target: 'bench', label: 'trial limits' },
        { source: 'journal', target: 'bench', label: 'recipe hints' },
        
        // Core internal flows
        { source: 'content.db', target: 'recipes', label: 'lookup' },
        { source: 'ignite', target: 'chain', label: 'resolve chain' },
        { source: 'run.state', target: 'depths', label: 'track depth' },
        
        // Content flows
        { source: 'materials', target: 'recipes', label: 'defines inputs' },
        { source: 'catalysts', target: 'events', label: 'modify outcomes' },
        { source: 'aspects', target: 'bench', label: 'scope effects' },
        
        // Game -> Core feedback
        { source: 'run.state', target: 'journal', label: 'update journal' },
        { source: 'cutscene.discovery', target: 'journal', label: 'save discovery' },
      ]
    };

    const width = document.body.clientWidth - 100;
    const height = document.body.clientHeight - 48;

    // Force-directed layout (force simulation)
    const simulation = d3.forceSimulation()
      .force('link', d3.forceLink(data.links))
      .force('charge', d3.forceMultiPulseCharges().strength(-500))
      .force('collide', d3.forceCollide().radius(40).strength(0.1))
      .force('center', d3.forceCenter(width * 0.3, height * 0.5));

    // Create the graph visualization
    const svg = d3.select("#graphCanvas");
    const ns = "http://www.w3.org/2000/svg";
    
    let g = svg.append("g");

    // Draw links first (behind nodes)
    const link = g.append("svg:g").attr("class", "links");
    data.links.forEach(d => {
      link.append("line")
        .attr("class", "link")
        .attr("x1", d.source.x)
        .attr("y1", d.source.y)
        .attr("x2", d.target.x)
        .attr("y2", d.target.y)
        .attr("stroke-width", 1.5);
    });

    // Create node circles
    const node = g.append("svg:g").attr("class", "nodes");
    
    data.nodes.forEach(d => {
      // Place node at random-ish position, then let simulation move it
      d.x = Math.random() * width;
      d.y = Math.random() * height;

      const circle = link.append("circle")
        .attr("class", `node-${d.type.toLowerCase()}`)
        .attr("cx", 0).attr("cy", 0).attr("r", 0);

      // Layer-specific colors
      const colorMap = {
        core: '#0c7ce8',
        game: '#e89b00',
        data: '#fbdd4d',
        content: ['#00bc7f', '#666', '#888'] // data, events, goals
      };

      const layerColor = colorMap[d.type] || '#666';

      circle
        .attr("fill", (i) => d.layer === 'core' ? '#1a7fff' : 
                       d.layer === 'game' ? '#e89b00' :
                       d.layer === 'content' ? '#fbdd4d' : '#666')

        // Use actual layer color for node circles
        .style("fill", d.type === 'core' ? '#1a7fff' : 
                     d.type === 'game' ? '#e89b00' :
                     d.type === 'data' ? '#fbdd4d' : '#666');

      // Add label text
      const text = link.append("text")
        .attr("class", "label")
        .attr("x", 12).attr("y", -10)
        .text(d.label)
        .style("fill", "#2d3436");

      // Add type indicator triangle
      link.append("polygon")
        .attr("points", `-${10},0 ${10},${-10} ${-10},${-10}`)
        .attr("fill", d.type === 'core' ? '#0c7ce8' :
                    d.type === 'game' ? '#e89b00' :
                    d.type === 'content' ? '#fbdd4d' : '#666')
        .style("fill-opacity", '0.5');
    });

    // Update simulation on resize
    window.addEventListener('resize', () => {
      width = document.body.clientWidth - 100;
      height = document.body.clientHeight - 48;
      simulation.force('center').forceX(width * 0.3).forceY(height * 0.5);
    });

    // Add interaction tooltips
    svg.on('mouseover', event => {
      const nodeId = d3.select('.node').attr('data-id') || null;
      if (nodeId) {
        document.getElementById('tooltip').innerHTML = `
          <strong>${d3.select(nodeId).text()}</strong><br/>
          Layer: ${d3.select(nodeId).style('fill')}<br/>
          Links: ${d.links.length} connections
        `;
        document.getElementById('tooltip').style.opacity = 1;
      }
    });

    svg.on('mouseout', () => {
      document.getElementById('tooltip').style.opacity = 0;
    });
  </script>
</body>
</html>
